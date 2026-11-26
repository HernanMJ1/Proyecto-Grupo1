using Azure.Core;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupo1.Models
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(120, ErrorMessage = "El nombre no puede exceder 120 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(30, ErrorMessage = "El teléfono no puede exceder 30 caracteres.")]
        public string Telefono { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        [StringLength(160, ErrorMessage = "El correo no puede exceder 160 caracteres.")]
        public string? Correo { get; set; }

        [StringLength(200, ErrorMessage = "La dirección no puede exceder 200 caracteres.")]
        public string? Direccion { get; set; }

        public ICollection<Repuesto> Repuestos { get; set; } = new List<Repuesto>();
    }
}
