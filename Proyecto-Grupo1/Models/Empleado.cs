using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class Empleado
    {
        [Key]
        public int IdEmpleado { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(160)]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string ContrasenaHash { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Especialidad { get; set; }

        public bool Estado { get; set; } = true;

        [Required]
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public Rol? Rol { get; set; }

        public ICollection<OrdenServicio> OrdenesAsignadas { get; set; } = new List<OrdenServicio>();
        public ICollection<Diagnostico> DiagnosticosRealizados { get; set; } = new List<Diagnostico>();
        public ICollection<MovimientoInventario> MovimientosInventario { get; set; } = new List<MovimientoInventario>();
    }
}