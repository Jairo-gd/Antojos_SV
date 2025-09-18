namespace AntojosSV.Models
{
    public class Categoria
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } = default!;
        public List<Comidas> Comidas { get; set; } = new();
    }
}
