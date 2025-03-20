using System.ComponentModel.DataAnnotations;

namespace AdquisicionesAPI.Models
{
    public class HistorialAdquisicion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AdquisicionId { get; set; } 

        [Required]
        public string CampoModificado { get; set; } = string.Empty; 

        [Required]
        public string ValorAnterior { get; set; } = string.Empty;

        [Required]
        public string ValorNuevo { get; set; } = string.Empty;

        [Required]
        public DateTime FechaModificacion { get; set; } = DateTime.UtcNow;

        [Required]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        public int AccionId { get; set; }
        public virtual Accion? Accion { get; set; }
    }
}
