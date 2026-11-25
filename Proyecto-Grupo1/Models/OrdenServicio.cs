using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class OrdenServicio
    {
        [Key]
        public int IdOrden { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroOrden { get; set; } = string.Empty;

        [Required]
        public DateTime FechaIngreso { get; set; } = DateTime.Now;

        public DateTime? FechaEntregaEstimada { get; set; }
        public DateTime? FechaEntregaReal { get; set; }

        [Required]
        [StringLength(500)]
        public string ProblemaReportado { get; set; } = string.Empty;

        [Required]
        [StringLength(35)]
        public string Estado { get; set; } = "Recibido";

        [StringLength(300)]
        public string? AccesoriosRecibidos { get; set; }

        [Required]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente? Cliente { get; set; }

        [Required]
        public int IdEquipo { get; set; }

        [ForeignKey("IdEquipo")]
        public Equipo? Equipo { get; set; }

        public int? IdTecnico { get; set; }

        [ForeignKey("IdTecnico")]
        public Empleado? Tecnico { get; set; }

        public Factura? Factura { get; set; }
        public ICollection<Diagnostico> Diagnosticos { get; set; } = new List<Diagnostico>();
        public ICollection<DetalleServicio> DetallesServicio { get; set; } = new List<DetalleServicio>();
        public ICollection<DetalleRepuesto> DetallesRepuesto { get; set; } = new List<DetalleRepuesto>();
    }
}