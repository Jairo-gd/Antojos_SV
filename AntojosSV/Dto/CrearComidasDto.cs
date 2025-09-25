namespace AntojosSV.Dto
{
    public record CrearComidasDto
    (
        int Id,
        int CategoriaId,
        string Nombre,
        decimal Precio,
        string Descripcion,
        string UrlImagen

    );
}
