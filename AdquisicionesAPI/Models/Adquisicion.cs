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

        // 🔹 Se usa solo el `Id`, sin `[Required]` en la entidad relacionada
        public int UnidadId { get; set; }
        public virtual Unidad? Unidad { get; set; }  // Permitir `null` para evitar validaciones

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

        public int ProveedorId { get; set; }
        public virtual Proveedor? Proveedor { get; set; }

        public string? Documentacion { get; set; }

        public int EstadoId { get; set; } = 1; // 🔹 Se asigna automáticamente
        public virtual Estado? Estado { get; set; }
    }
}
