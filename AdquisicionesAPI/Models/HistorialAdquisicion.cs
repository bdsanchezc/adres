using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdquisicionesAPI.Models
{
    public class HistorialAdquisicion
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AdquisicionId { get; set; }
        [Required]
        public string DatosAnteriores { get; set; } = string.Empty;
        public DateTime FechaCambio { get; set; } = DateTime.UtcNow;

        [ForeignKey("AdquisicionId")]
        public Adquisicion? Adquisicion { get; set; }
    }
}
