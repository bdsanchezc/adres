using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdquisicionesAPI.Models
{
    public class Adquisicion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Presupuesto { get; set; }
        [Required, ForeignKey("Unidad")]
        public int UnidadId { get; set; }
        [Required]
        public string TipoBienServicio { get; set; } = string.Empty;
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal ValorUnitario { get; set; }
        [Required]
        public decimal ValorTotal { get; set; }
        [Required]
        public string FechaAdquisicion { get; set; } = string.Empty;
        [Required, ForeignKey("Proveedor")]
        public int ProveedorId { get; set; }
        public string? Documentacion { get; set; }
        [Required, ForeignKey("Estado")]
        public int EstadoId { get; set; } = 0;
    }
}
