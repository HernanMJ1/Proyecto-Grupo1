using Azure.Core;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupo1.Models
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [Required]
        [StringLength(120)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Telefono { get; set; } = string.Empty;

        [EmailAddress]
        [StringLength(160)]
        public string? Correo { get; set; }

        [StringLength(200)]
        public string? Direccion { get; set; }

        public ICollection<Repuesto> Repuestos { get; set; } = new List<Repuesto>();
    }
}