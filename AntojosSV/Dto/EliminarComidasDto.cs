namespace AntojosSV.Dto
{
    public record EliminarComidasDto
    (
        int Id,
        int CategoriaId,
        string Nombre,
        decimal Precio,
        string Descripcion,
        string UrlImagen
    );
}
