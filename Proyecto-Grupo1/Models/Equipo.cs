using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class Equipo
    {
        [Key]
        public int IdEquipo { get; set; }

        [Required]
        [StringLength(80)]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        public string Marca { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        [StringLength(120)]
        public string NumSerie { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Descripcion { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        public ICollection<OrdenServicio> OrdenesServicio { get; set; } = new List<OrdenServicio>();
    }
}