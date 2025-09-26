using AntojosSV.Data;
using AntojosSV.Dto;
using AntojosSV.Models;
using Microsoft.EntityFrameworkCore;

namespace AntojosSV.Endpoints
{
    public static class EncargosEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/encargos").WithTags("Encargos");

            group.MapPost("/", async (AntojosSVDb db, CrearEncargosDto dto) => {
                var errores = new Dictionary<string, string[]>();

                if (dto.Id == int.MinValue)
                    errores["id"] = ["El id es requerida."];

                if (dto.UsuarioId == int.MinValue)
                    errores["usuarioid"] = ["El id es requerida."];

                if (dto.ComidasId == int.MinValue)
                    errores["comidasid"] = ["El id es requerida."];

                if (string.IsNullOrWhiteSpace(dto.Direccion))
                    errores["direccion"] = ["La direccion es requerida."];

                if (string.IsNullOrWhiteSpace(dto.Telefono))
                    errores["telefono"] = ["El telefono es requerido."];

                if (dto.FechaEntrega == DateTime.MinValue)
                {
                    errores["fechaentrega"] = ["La fecha de entrega es requerida."];
                }

                if (errores.Count > 0) return Results.ValidationProblem(errores);

                var entity = new Encargos
                {
                    Id = dto.Id,
                    UsuarioId = dto.UsuarioId,
                    ComidasId = dto.ComidasId,
                    Direccion = dto.Direccion,
                    Telefono = dto.Telefono,
                    FechaEntrega = dto.FechaEntrega,
                };

                db.Encargos.Add(entity);
                await db.SaveChangesAsync();

                var dtoSalida = new EncargosDto(
                    entity.Id,
                    entity.UsuarioId,
                    entity.ComidasId,
                    entity.Direccion,
                    entity.Telefono,
                    (DateTime)entity.FechaEntrega);

                return Results.Created($"/encargos/{entity.Id}", dtoSalida);
            });

            group.MapGet("/", async (AntojosSVDb db) => {

                var consulta = await db.Encargos.ToListAsync();

                var encargos = consulta.Select(e => new EncargosDto(
                    e.Id,
                    e.UsuarioId,
                    e.ComidasId,
                    e.Direccion,
                    e.Telefono,
                    e.FechaEntrega.Value
                ))
                .OrderBy(e => e.Direccion)
                .ToList();

                return Results.Ok(encargos);
            });
            group.MapGet("/{id}", async (int id, AntojosSVDb db) => {
                var encargos = await db.Encargos
                .Where(e => e.Id == id)
                .Select(e => new EncargosDto(
                    e.Id,
                    e.UsuarioId,
                    e.ComidasId,
                    e.Direccion,
                    e.Telefono,
                    e.FechaEntrega.Value

                )).FirstOrDefaultAsync();

                return Results.Ok(encargos);
            });

            group.MapPut("/{id}", async (int id, ModificarEncargosDto dto, AntojosSVDb db) => {
                var encargos = await db.Encargos.FindAsync(id);

                if (encargos is null)
                    return Results.NotFound();

                encargos.Direccion = dto.Direccion;
                encargos.Telefono = dto.Telefono;
                encargos.FechaEntrega = dto.FechaEntrega;

                await db.SaveChangesAsync();

                return Results.NoContent();

            });

            group.MapDelete("/{id}", async (int id, AntojosSVDb db) =>
            {
                var encargos = await db.Encargos.FindAsync(id);
                if (encargos is null)
                    return Results.NotFound();

                db.Remove(encargos);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });


        }
    }
}
