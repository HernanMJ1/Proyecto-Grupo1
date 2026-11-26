using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_Grupo1.Models
{
    public class DiagnosticoOrden
    {
        [Key]
        public int DiagnosticoId { get; set; }

        // Vincula con tu entidad de Orden existente (ajusta el nombre si tu PK no es OrdenId)
        [Required]
        public int OrdenId { get; set; }

        [Required(ErrorMessage = "El diagnóstico técnico es obligatorio.")]
        [Display(Name = "Diagnóstico Técnico")]
        public string DiagnosticoTecnico { get; set; } = string.Empty;

        [Display(Name = "Observaciones / Riesgos")]
        public string? Observaciones { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Costo Mano de Obra")]
        public decimal CostoManoObra { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total Estimado")]
        public decimal TotalEstimado { get; set; }

        public virtual ICollection<CostoEstimadoItem> CostosEstimados { get; set; } = new List<CostoEstimadoItem>();
    }

    public class CostoEstimadoItem
    {
        [Key]
        public int CostoEstimadoItemId { get; set; }

        public int DiagnosticoId { get; set; }

        [ForeignKey("DiagnosticoId")]
        public virtual DiagnosticoOrden? Diagnostico { get; set; }

        [Required]
        [Display(Name = "Repuesto / Concepto")]
        public string Concepto { get; set; } = string.Empty;

        [Required]
        public int Cantidad { get; set; } = 1;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [NotMapped]
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
