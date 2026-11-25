using System.ComponentModel.DataAnnotations;

namespace Proyecto_Grupo1.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(80)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Descripcion { get; set; }

        public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}