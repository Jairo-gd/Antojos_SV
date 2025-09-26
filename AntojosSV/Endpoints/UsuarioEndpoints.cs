using AntojosSV.Data;
using AntojosSV.Dto;
using AntojosSV.Models;
using Microsoft.EntityFrameworkCore;

namespace AntojosSV.Endpoints
{
    public static class UsuarioEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/usuario").WithTags("Usuarios");

            group.MapPost("/", async (AntojosSVDb db, CrearUsuariosDto dto) => {
                var errores = new Dictionary<string, string[]>();

                if (dto.Id == int.MinValue)
                    errores["id"] = ["El id es requerida."];

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    errores["nombre"] = ["El nombre es requerida."];

                if (string.IsNullOrWhiteSpace(dto.Apellido))
                    errores["apellido"] = ["El apellido es requerido."];

                if (string.IsNullOrWhiteSpace(dto.Telefono))
                    errores["telefono"] = ["El telefono es requerido."];

                if (string.IsNullOrWhiteSpace(dto.CorreoElectronico))
                    errores["correoelectronico"] = ["El correoelectronico es requerido."];

                if (errores.Count > 0) return Results.ValidationProblem(errores);

                var entity = new Usuario
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    Apellido = dto.Apellido,
                    Telefono = dto.Telefono,
                    CorreoElectronico = dto.CorreoElectronico,
                };

                db.Usuarios.Add(entity);
                await db.SaveChangesAsync();

                var dtoSalida = new UsuarioDto(
                    entity.Id,
                    entity.Nombre,
                    entity.Apellido,
                    entity.Telefono,
                    entity.CorreoElectronico
                );

                return Results.Created($"/usuarios/{entity.Id}", dtoSalida);
            });

            group.MapGet("/", async (AntojosSVDb db) => {

                var consulta = await db.Usuarios.ToListAsync();

                var usuario = consulta.Select(u => new UsuarioDto(
                    u.Id,
                    u.Nombre,
                    u.Apellido,
                    u.Telefono,
                    u.CorreoElectronico
                ))

                .OrderBy(u => u.Nombre)
                .ToList();

                return Results.Ok(usuario);
            });
            group.MapGet("/{id}", async (int id, AntojosSVDb db) => {
                var usuarios = await db.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new UsuarioDto(
                    u.Id,
                    u.Nombre,
                    u.Apellido,
                    u.Telefono,
                    u.CorreoElectronico

                )).FirstOrDefaultAsync();

                return Results.Ok(usuarios);
            });

            group.MapPut("/{id}", async (int id, ModificarUsuarioDto dto, AntojosSVDb db) => {
                var usuario = await db.Usuarios.FindAsync(id);

                if (usuario is null)
                    return Results.NotFound();

                usuario.Nombre = dto.Nombre;
                usuario.Apellido = dto.Apellido;
                usuario.Telefono = dto.Telefono;
                usuario.CorreoElectronico = dto.CorreoElectronico;

                await db.SaveChangesAsync();

                return Results.NoContent();

            });

            group.MapDelete("/{id}", async (int id, AntojosSVDb db) =>
            {
                var usuario = await db.Usuarios.FindAsync(id);
                if (usuario is null)
                    return Results.NotFound();

                db.Remove(usuario);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
