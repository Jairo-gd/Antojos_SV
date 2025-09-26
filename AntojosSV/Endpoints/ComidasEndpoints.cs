using AntojosSV.Data;
using AntojosSV.Dto;
using AntojosSV.Models;
using Microsoft.EntityFrameworkCore;


namespace AntojosSV.Endpoints
{
    public  static class ComidasEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/comidas").WithTags("Comidas");

            group.MapPost("/", async (AntojosSVDb db, CrearComidasDto dto) => {
                var errores = new Dictionary<string, string[]>();

                if (dto.Id == int.MinValue)
                    errores["id"] = ["El id es requerido."];

                if (dto.CategoriaId == int.MinValue)
                    errores["categoriaid"] = ["El id es requerida."];

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    errores["nombre"] = ["El nombre es  requerido."];

                if (dto.Precio <= 0)
                    errores["precio"] = ["El precio es requerido."];

                if (string.IsNullOrWhiteSpace(dto.Descripcion))
                    errores["telefono"] = ["El telefono es requerido."];

                if (string.IsNullOrWhiteSpace(dto.UrlImagen))
                    errores["urlimagen"] = ["El urlimagen es requerido."];



                if (errores.Count > 0) return Results.ValidationProblem(errores);

                var entity = new Comidas
                {
                    Id = dto.Id,
                    CategoriaId = dto.CategoriaId,
                    Nombre = dto.Nombre,
                    Precio = dto.Precio,
                    Descripcion = dto.Descripcion,
                    UrlImagen = dto.UrlImagen,
                };

                db.Comidas.Add(entity);
                await db.SaveChangesAsync();

                var dtoSalida = new ComidasDto(
                    entity.Id,
                    entity.CategoriaId,
                    entity.Nombre,
                    entity.Precio,
                    entity.Descripcion,
                    entity.UrlImagen
                );

                return Results.Created($"/comidas/{entity.Id}", dtoSalida);
            });

            group.MapGet("/", async (AntojosSVDb db) => {

                var consulta = await db.Comidas.ToListAsync();

                var comidas = consulta.Select(c => new ComidasDto(
                    c.Id,
                    c.CategoriaId,
                    c.Nombre,
                    c.Precio,
                    c.Descripcion,
                    c.UrlImagen
                ))
                .OrderBy(c => c.CategoriaId)
                .ToList();

                return Results.Ok(comidas);
            });
            group.MapGet("/{id}", async (int id, AntojosSVDb db) => {
                var comidas = await db.Comidas
                .Where(c => c.Id == id)
                .Select(c => new ComidasDto(
                    c.Id,
                    c.CategoriaId,
                    c.Nombre,
                    c.Precio,
                    c.Descripcion,
                    c.UrlImagen

                )).FirstOrDefaultAsync();

                return Results.Ok(comidas);
            });

            group.MapPut("/{id}", async (int id, ModificarComidasDto dto, AntojosSVDb db) => {
                var comidas = await db.Comidas.FindAsync(id);

                if (comidas is null)
                    return Results.NotFound();

                comidas.Nombre = dto.Nombre;
                comidas.Precio = dto.Precio;
                comidas.Descripcion = dto.Descripcion;
                comidas.UrlImagen = dto.UrlImagen;

                await db.SaveChangesAsync();

                return Results.NoContent();

            });
            group.MapDelete("/{id}", async (int id, AntojosSVDb db) =>
            {
                var comidas = await db.Comidas.FindAsync(id);
                if (comidas is null)
                    return Results.NotFound();

                db.Remove(comidas);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

        }
    }
}
