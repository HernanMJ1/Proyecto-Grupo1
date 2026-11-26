using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Proyecto_Grupo1.Data;
using Proyecto_Grupo1.Models;

namespace Proyecto_Grupo1.Controllers;

public class RepuestosController : Controller
{
    private const string MensajeCodigoDuplicado = "Ya existe un repuesto con este código.";
    private readonly ApplicationDbContext _context;
    public RepuestosController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index(string? busqueda)
    {
        var consulta = _context.Repuestos.Include(r => r.Proveedor).AsNoTracking();
        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            busqueda = busqueda.Trim();
            consulta = consulta.Where(r => r.Codigo.Contains(busqueda) || r.Nombre.Contains(busqueda));
        }
        ViewData["Busqueda"] = busqueda;
        return View(await consulta.OrderBy(r => r.Nombre).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var repuesto = await _context.Repuestos.Include(r => r.Proveedor).AsNoTracking()
            .FirstOrDefaultAsync(r => r.IdRepuesto == id);
        return repuesto == null ? NotFound() : View(repuesto);
    }

    public async Task<IActionResult> Create()
    {
        await CargarProveedoresAsync();
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Codigo,Nombre,Descripcion,Stock,StockMinimo,PrecioCompra,PrecioVenta,IdProveedor")] Repuesto repuesto)
    {
        Normalizar(repuesto);
        ValidarCamposObligatorios(repuesto);
        await CargarProveedoresParaGuardadoAsync(repuesto.IdProveedor);
        await ValidarCodigoAsync(repuesto.Codigo);
        if (ModelState.IsValid)
        {
            try
            {
                _context.Add(repuesto);
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "El repuesto se registró correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException excepcion)
            {
                AgregarErrorDeGuardado(excepcion, "No se pudo registrar el repuesto. Intente nuevamente.");
            }
        }
        return View(repuesto);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var repuesto = await _context.Repuestos.FindAsync(id);
        if (repuesto == null) return NotFound();
        await CargarProveedoresAsync(repuesto.IdProveedor);
        return View(repuesto);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("IdRepuesto,Codigo,Nombre,Descripcion,Stock,StockMinimo,PrecioCompra,PrecioVenta,IdProveedor")] Repuesto repuesto)
    {
        if (id != repuesto.IdRepuesto) return NotFound();
        Normalizar(repuesto);
        ValidarCamposObligatorios(repuesto);
        await CargarProveedoresParaGuardadoAsync(repuesto.IdProveedor);
        await ValidarCodigoAsync(repuesto.Codigo, repuesto.IdRepuesto);
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(repuesto);
                await _context.SaveChangesAsync();
                TempData["MensajeExito"] = "El repuesto se actualizó correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Repuestos.AnyAsync(r => r.IdRepuesto == id)) return NotFound();
                ModelState.AddModelError(string.Empty, "No se pudo actualizar el repuesto. Intente nuevamente.");
            }
            catch (DbUpdateException excepcion)
            {
                AgregarErrorDeGuardado(excepcion, "No se pudo actualizar el repuesto. Intente nuevamente.");
            }
        }
        return View(repuesto);
    }

    private void AgregarErrorDeGuardado(DbUpdateException excepcion, string mensaje)
    {
        if (excepcion.InnerException is SqlException errorSql && errorSql.Errors.Cast<SqlError>().Any(error =>
            error.Number is 2601 or 2627 && error.Message.Contains("'IX_Repuestos_Codigo'", StringComparison.Ordinal)))
        {
            ModelState.AddModelError(nameof(Repuesto.Codigo), MensajeCodigoDuplicado);
            return;
        }

        ModelState.AddModelError(string.Empty, mensaje);
    }

    private async Task ValidarCodigoAsync(string codigo, int? idActual = null)
    {
        if (!string.IsNullOrWhiteSpace(codigo) && await _context.Repuestos
            .AnyAsync(r => r.Codigo == codigo && (!idActual.HasValue || r.IdRepuesto != idActual.Value)))
            ModelState.AddModelError(nameof(Repuesto.Codigo), MensajeCodigoDuplicado);
    }

    private async Task CargarProveedoresParaGuardadoAsync(int idProveedor)
    {
        var proveedores = await CargarProveedoresAsync(idProveedor);
        if (idProveedor > 0 && !proveedores.Any(p => p.IdProveedor == idProveedor))
            ModelState.AddModelError(nameof(Repuesto.IdProveedor), "El proveedor seleccionado no existe.");
    }

    private async Task<List<Proveedor>> CargarProveedoresAsync(int? seleccionado = null)
    {
        var proveedores = await _context.Proveedores.AsNoTracking().OrderBy(p => p.Nombre).ToListAsync();
        ViewBag.IdProveedor = new SelectList(proveedores,
            nameof(Proveedor.IdProveedor), nameof(Proveedor.Nombre), seleccionado);
        return proveedores;
    }

    private static void Normalizar(Repuesto repuesto)
    {
        repuesto.Codigo = repuesto.Codigo?.Trim() ?? string.Empty;
        repuesto.Nombre = repuesto.Nombre?.Trim() ?? string.Empty;
        repuesto.Descripcion = string.IsNullOrWhiteSpace(repuesto.Descripcion) ? null : repuesto.Descripcion.Trim();
    }

    private void ValidarCamposObligatorios(Repuesto repuesto)
    {
        if (string.IsNullOrWhiteSpace(repuesto.Codigo))
            ModelState.AddModelError(nameof(Repuesto.Codigo), "El código es obligatorio.");
        if (string.IsNullOrWhiteSpace(repuesto.Nombre))
            ModelState.AddModelError(nameof(Repuesto.Nombre), "El nombre es obligatorio.");
    }
}
