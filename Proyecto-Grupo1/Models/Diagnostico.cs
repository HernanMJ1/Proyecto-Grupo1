using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class Diagnostico
    {
        [Key]
        public int IdDiagnostico { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [StringLength(600)]
        public string Descripcion { get; set; } = string.Empty;

        [Column(TypeName = "decimal(12,2)")]
        public decimal CostoEstimado { get; set; }

        public int? TiempoEstimadoDias { get; set; }

        public bool? Aprobado { get; set; }

        [StringLength(300)]
        public string? ObservacionesCliente { get; set; }

        [Required]
        public int IdOrden { get; set; }

        [ForeignKey("IdOrden")]
        public OrdenServicio? OrdenServicio { get; set; }

        [Required]
        public int IdTecnico { get; set; }

        [ForeignKey("IdTecnico")]
        public Empleado? Tecnico { get; set; }
    }
}