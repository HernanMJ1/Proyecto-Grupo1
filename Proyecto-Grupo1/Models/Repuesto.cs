using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class Repuesto
    {
        [Key]
        public int IdRepuesto { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no puede exceder 20 caracteres.")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(120, ErrorMessage = "El nombre no puede exceder 120 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "La descripción no puede exceder 255 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El stock mínimo es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo.")]
        public int StockMinimo { get; set; }

        [Required(ErrorMessage = "El precio de compra es obligatorio.")]
        [Range(typeof(decimal), "0", "9999999999.99", ErrorMessage = "El precio de compra no puede ser negativo.")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal PrecioCompra { get; set; }

        [Required(ErrorMessage = "El precio de venta es obligatorio.")]
        [Range(typeof(decimal), "0", "9999999999.99", ErrorMessage = "El precio de venta no puede ser negativo.")]
        [Column(TypeName = "decimal(12,2)")]
        public decimal PrecioVenta { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un proveedor.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un proveedor.")]
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        public Proveedor? Proveedor { get; set; }

        public ICollection<DetalleRepuesto> DetallesRepuesto { get; set; } = new List<DetalleRepuesto>();
        public ICollection<MovimientoInventario> MovimientosInventario { get; set; } = new List<MovimientoInventario>();
    }
}
