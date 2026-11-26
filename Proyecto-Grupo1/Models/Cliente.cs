using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupo1.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Identidad { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        [StringLength(160)]
        [Display(Name = "Correo electrónico")]
        public string? Correo { get; set; }

        [StringLength(250)]
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }

        public bool Estado { get; set; } = true;

        public ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
        public ICollection<OrdenServicio> OrdenesServicio { get; set; } = new List<OrdenServicio>();
    }
}