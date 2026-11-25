using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class MovimientoInventario
    {
        [Key]
        public int IdMovimiento { get; set; }

        [Required]
        [StringLength(20)]
        public string Tipo { get; set; } = string.Empty; // ENTRADA o SALIDA

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [StringLength(250)]
        public string? Motivo { get; set; }

        [Required]
        public int IdRepuesto { get; set; }

        [ForeignKey("IdRepuesto")]
        public Repuesto? Repuesto { get; set; }

        [Required]
        public int IdEmpleado { get; set; }

        [ForeignKey("IdEmpleado")]
        public Empleado? Empleado { get; set; }

        public int? IdOrden { get; set; }

        [ForeignKey("IdOrden")]
        public OrdenServicio? OrdenServicio { get; set; }
    }
}