using System.ComponentModel.DataAnnotations;

namespace AntojosSV.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; } = default!;
        [MaxLength(50)]
        public string Apellido { get; set; } = default!;
        [MaxLength(12)]
        public string Telefono { get; set; } = default!;
        [MaxLength(120)]
        public string CorreoElectronico { get; set; } = default!;

        public List<Encargos> Encargos { get; set; } = new();
    }
}
