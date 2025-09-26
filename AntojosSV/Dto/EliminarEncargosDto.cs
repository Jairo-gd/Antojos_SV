namespace AntojosSV.Dto
{
    public record EliminarEncargosDto
     (
        int Id,
        string Direccion,
        string Telefono,
        DateTime FechaEntrega
    );
}
