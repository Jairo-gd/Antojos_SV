namespace AntojosSV.Dto
{
    public record ComidasDto
    (
         int Id,
         int CategoriaId,
         string Nombre,
         decimal Precio,
         string Descripcion,
         string UrlImagen
    );
}
