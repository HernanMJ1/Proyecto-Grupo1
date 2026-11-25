using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Monto { get; set; }

        [Required]
        [StringLength(30)]
        public string Metodo { get; set; } = string.Empty; // Efectivo, Tarjeta, Transferencia

        [StringLength(100)]
        public string? Referencia { get; set; }

        [Required]
        public int IdFactura { get; set; }

        [ForeignKey("IdFactura")]
        public Factura? Factura { get; set; }
    }
}