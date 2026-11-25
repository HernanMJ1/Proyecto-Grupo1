using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Subtotal { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Impuesto { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Total { get; set; }

        [Required]
        [StringLength(25)]
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Pagada, Anulada

        [Required]
        public int IdOrden { get; set; }

        [ForeignKey("IdOrden")]
        public OrdenServicio? OrdenServicio { get; set; }

        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}