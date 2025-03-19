using System.ComponentModel.DataAnnotations;

namespace AdquisicionesAPI.Models
{
    public class Unidad
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
    }
}