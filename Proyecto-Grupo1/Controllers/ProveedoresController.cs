using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Grupo1.Data;
using Proyecto_Grupo1.Models;

namespace Proyecto_Grupo1.Controllers;

public class ProveedoresController : Controller
{
    private readonly ApplicationDbContext _context;
    public ProveedoresController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index(string? busqueda)
    {
        var consulta = _context.Proveedores.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            busqueda = busqueda.Trim();
            consulta = consulta.Where(p => p.Nombre.Contains(busqueda));
        }
        ViewData["Busqueda"] = busqueda;
        return View(await consulta.OrderBy(p => p.Nombre).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var proveedor = await _context.Proveedores.AsNoTracking().FirstOrDefaultAsync(p => p.IdProveedor == id);
        return proveedor == null ? NotFound() : View(proveedor);
    }

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Telefono,Correo,Direccion")] Proveedor proveedor)
    {
        Normalizar(proveedor);
        ValidarCamposObligatorios(proveedor);
        if (!ModelState.IsValid) return View(proveedor);
        _context.Add(proveedor);
        await _context.SaveChangesAsync();
        TempData["MensajeExito"] = "El proveedor se registró correctamente.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var proveedor = await _context.Proveedores.FindAsync(id);
        return proveedor == null ? NotFound() : View(proveedor);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("IdProveedor,Nombre,Telefono,Correo,Direccion")] Proveedor proveedor)
    {
        if (id != proveedor.IdProveedor) return NotFound();
        Normalizar(proveedor);
        ValidarCamposObligatorios(proveedor);
        if (!ModelState.IsValid) return View(proveedor);
        try
        {
            _context.Update(proveedor);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Proveedores.AnyAsync(p => p.IdProveedor == id)) return NotFound();
            throw;
        }
        TempData["MensajeExito"] = "El proveedor se actualizó correctamente.";
        return RedirectToAction(nameof(Index));
    }

    private static void Normalizar(Proveedor proveedor)
    {
        proveedor.Nombre = proveedor.Nombre?.Trim() ?? string.Empty;
        proveedor.Telefono = proveedor.Telefono?.Trim() ?? string.Empty;
        proveedor.Correo = string.IsNullOrWhiteSpace(proveedor.Correo) ? null : proveedor.Correo.Trim();
        proveedor.Direccion = string.IsNullOrWhiteSpace(proveedor.Direccion) ? null : proveedor.Direccion.Trim();
    }

    private void ValidarCamposObligatorios(Proveedor proveedor)
    {
        if (string.IsNullOrWhiteSpace(proveedor.Nombre))
            ModelState.AddModelError(nameof(Proveedor.Nombre), "El nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(proveedor.Telefono))
            ModelState.AddModelError(nameof(Proveedor.Telefono), "El teléfono es obligatorio.");
    }
}
