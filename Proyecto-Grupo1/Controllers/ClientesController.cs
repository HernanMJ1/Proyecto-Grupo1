using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Grupo1.Data;
using Proyecto_Grupo1.Models;

namespace Proyecto_Grupo1.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes?busqueda=&estado=
        [HttpGet]
        public async Task<IActionResult> Index(string? busqueda, bool? estado)
        {
            IQueryable<Cliente> query = _context.Clientes;

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                string termino = busqueda.Trim();
                query = query.Where(c =>
                    c.Nombre.Contains(termino) ||
                    c.Identidad.Contains(termino) ||
                    c.Telefono.Contains(termino));
            }

            if (estado.HasValue)
            {
                query = query.Where(c => c.Estado == estado.Value);
            }

            ViewData["Busqueda"] = busqueda;
            ViewData["Estado"] = estado;

            var clientes = await query.OrderBy(c => c.Nombre).ToListAsync();
            return View(clientes);
        }

        // GET: Clientes/Details/5 (ficha del cliente + equipos y órdenes asociados)
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Equipos)
                .Include(c => c.OrdenesServicio)
                    .ThenInclude(o => o.Equipo)
                .FirstOrDefaultAsync(c => c.IdCliente == id);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.OrdenesServicio = cliente.OrdenesServicio
                .OrderByDescending(o => o.FechaIngreso)
                .ToList();

            return View(cliente);
        }

        // GET: Clientes/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nombre,Identidad,Telefono,Correo,Direccion")] Cliente cliente)
        {
            if (await ExisteIdentidad(cliente.Identidad, cliente.IdCliente))
            {
                ModelState.AddModelError("Identidad", "Ya existe un cliente registrado con esa identidad.");
            }

            cliente.Estado = true;

            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            TempData["GuardadoExito"] = "Cliente registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("IdCliente,Nombre,Identidad,Telefono,Correo,Direccion")] Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return NotFound();
            }

            if (await ExisteIdentidad(cliente.Identidad, cliente.IdCliente))
            {
                ModelState.AddModelError("Identidad", "Ya existe otro cliente registrado con esa identidad.");
            }

            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            var clienteExistente = await _context.Clientes.FindAsync(id);
            if (clienteExistente == null)
            {
                return NotFound();
            }

            // Solo se actualizan los datos personales; Estado y relaciones se conservan.
            clienteExistente.Nombre = cliente.Nombre;
            clienteExistente.Identidad = cliente.Identidad;
            clienteExistente.Telefono = cliente.Telefono;
            clienteExistente.Correo = string.IsNullOrWhiteSpace(cliente.Correo) ? null : cliente.Correo.Trim();
            clienteExistente.Direccion = string.IsNullOrWhiteSpace(cliente.Direccion) ? null : cliente.Direccion.Trim();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Clientes.AnyAsync(c => c.IdCliente == id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData["GuardadoExito"] = "Cliente actualizado correctamente.";
            return RedirectToAction(nameof(Details), new { id = cliente.IdCliente });
        }

        // POST: Clientes/CambiarEstado/5 (desactivación lógica, RF02 — nunca borra registros)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CambiarEstado(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            cliente.Estado = !cliente.Estado;
            await _context.SaveChangesAsync();

            TempData["GuardadoExito"] = cliente.Estado
                ? "Cliente activado correctamente."
                : "Cliente desactivado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ExisteIdentidad(string identidad, int idCliente)
        {
            string valor = identidad?.Trim() ?? string.Empty;
            if (valor.Length == 0)
            {
                return false;
            }

            return await _context.Clientes
                .AnyAsync(c => c.Identidad == valor && c.IdCliente != idCliente);
        }
    }
}
