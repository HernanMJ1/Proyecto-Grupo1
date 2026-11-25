using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class DetalleServicio
    {
        [Key]
        public int IdDetalleServ { get; set; }

        [Required]
        public int Cantidad { get; set; } = 1;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Precio { get; set; }

        [Required]
        public int IdOrden { get; set; }

        [ForeignKey("IdOrden")]
        public OrdenServicio? OrdenServicio { get; set; }

        [Required]
        public int IdServicio { get; set; }

        [ForeignKey("IdServicio")]
        public Servicio? Servicio { get; set; }
    }
}