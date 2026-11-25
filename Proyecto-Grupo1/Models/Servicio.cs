using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class Servicio
    {
        [Key]
        public int IdServicio { get; set; }

        [Required]
        [StringLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal PrecioBase { get; set; }

        public ICollection<DetalleServicio> DetallesServicio { get; set; } = new List<DetalleServicio>();
    }
}