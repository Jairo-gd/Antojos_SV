namespace AntojosSV.Dto
{
    public record EncargosDto
    (
        int Id,
        int UsuarioId,
        int ComidasId,
        string Direccion,
        string Telefono,
        DateTime FechaEntrega
    );
}
