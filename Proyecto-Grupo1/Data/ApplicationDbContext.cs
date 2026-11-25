using Microsoft.EntityFrameworkCore;
using Proyecto_Grupo1.Models;

namespace Proyecto_Grupo1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<OrdenServicio> OrdenesServicio { get; set; }
        public DbSet<Diagnostico> Diagnosticos { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<DetalleServicio> DetallesServicio { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Repuesto> Repuestos { get; set; }
        public DbSet<DetalleRepuesto> DetallesRepuesto { get; set; }
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Índices únicos
            modelBuilder.Entity<Empleado>().HasIndex(e => e.Usuario).IsUnique();
            modelBuilder.Entity<Empleado>().HasIndex(e => e.Correo).IsUnique();
            modelBuilder.Entity<Cliente>().HasIndex(c => c.Identidad).IsUnique();
            modelBuilder.Entity<Equipo>().HasIndex(eq => eq.NumSerie).IsUnique();
            modelBuilder.Entity<OrdenServicio>().HasIndex(o => o.NumeroOrden).IsUnique();
            modelBuilder.Entity<Repuesto>().HasIndex(r => r.Codigo).IsUnique();
            modelBuilder.Entity<Factura>().HasIndex(f => f.IdOrden).IsUnique();

            // Evitar múltiples rutas de borrado en cascada en SQL Server
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Datos semilla iniciales (Roles definidos en el alcance)
            modelBuilder.Entity<Rol>().HasData(
                new Rol { IdRol = 1, Nombre = "Administrador", Descripcion = "Gestiona usuarios, roles, inventario, proveedores y reportes" },
                new Rol { IdRol = 2, Nombre = "Recepcionista", Descripcion = "Registra clientes y equipos, crea órdenes, factura y entrega equipos" },
                new Rol { IdRol = 3, Nombre = "Técnico", Descripcion = "Registra diagnósticos, cotizaciones, estado de reparación y repuestos usados" },
                new Rol { IdRol = 4, Nombre = "Cliente", Descripcion = "Consulta el estado de su orden en línea y aprueba o rechaza cotizaciones" }
            );
        }
    }
}