using System.ComponentModel.DataAnnotations;

namespace AntojosSV.Models
{
    public class Encargos
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public int ComidasId { get; set; } 
        public Comidas? Comidas { get; set; }
        [MaxLength(120)]
        public string Direccion { get; set; } = default!;
        [MaxLength(12)]
        public string Telefono { get; set; } = default!;
        public DateTime? FechaEntrega { get; set; } = default!;

    }
}
