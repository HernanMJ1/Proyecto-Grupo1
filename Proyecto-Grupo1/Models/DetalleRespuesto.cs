using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class DetalleRepuesto
    {
        [Key]
        public int IdDetalleRep { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Precio { get; set; }

        [Required]
        public int IdOrden { get; set; }

        [ForeignKey("IdOrden")]
        public OrdenServicio? OrdenServicio { get; set; }

        [Required]
        public int IdRepuesto { get; set; }

        [ForeignKey("IdRepuesto")]
        public Repuesto? Repuesto { get; set; }
    }
}