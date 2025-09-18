using System.ComponentModel.DataAnnotations;

namespace AntojosSV.Models
{
    public class Comidas
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; } = default!;
        public decimal Precio { get; set; } = default!;
        [MaxLength(200)]
        public string Descripcion { get; set; } = default!;
        public string UrlImagen { get; set; } = default!;

        public List<Encargos> Encargos { get; set; } = new();


    }
}