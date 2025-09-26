using AntojosSV.Data;
using AntojosSV.Dto;
using AntojosSV.Models;
using Microsoft.EntityFrameworkCore;

namespace AntojosSV.Endpoints
{
    public static class CategoriaEndpoints
    {
        public static void Add(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/categoria").WithTags("Categoria");

            group.MapPost("/", async (AntojosSVDb db, CrearCategoriaDto dto) => {
                var errores = new Dictionary<string, string[]>();

                if (dto.Id == int.MinValue)
                    errores["id"] = ["El id es requerida."];

                if (string.IsNullOrWhiteSpace(dto.Nombre))
                    errores["direccion"] = ["La direccion es requerida."];
                
                if (errores.Count > 0) return Results.ValidationProblem(errores);

                var entity = new Categoria
                {
                    Id = dto.Id,
                    Nombre = dto.Nombre,
                    
                   
                };

                db.Categorias.Add(entity);
                await db.SaveChangesAsync();

                var dtoSalida = new CategoriaDto(
                    entity.Id,
                    entity.Nombre
                   );
         

                return Results.Created($"/categoria/{entity.Id}", dtoSalida);
            });

            group.MapGet("/", async (AntojosSVDb db) => {

                var consulta = await db.Categorias.ToListAsync();

                var categoria= consulta.Select(e => new CategoriaDto(
                    e.Id,
                    e.Nombre
                    
                ))
                .OrderBy(e => e.Nombre)
                .ToList();

                return Results.Ok(categoria);
            });
            group.MapGet("/{id}", async (int id, AntojosSVDb db) => {
            var categoria = await db.Categorias
            .Where(e => e.Id == id)
            .Select(e => new CategoriaDto(
                e.Id,
                e.Nombre


                )).FirstOrDefaultAsync();

                return Results.Ok(categoria);
            });

            group.MapPut("/{id}", async (int id, ModificarCategoriaDto dto, AntojosSVDb db) => {
                var categoria = await db.Categorias.FindAsync(id);

                if (categoria is null)
                    return Results.NotFound();

                categoria.Nombre = dto.Nombre;
                

                await db.SaveChangesAsync();

                return Results.NoContent();

            });

            group.MapDelete("/{id}", async (int id, AntojosSVDb db) =>
            {
                var categoria = await db.Categorias.FindAsync(id);
                if (categoria is null)
                    return Results.NotFound();

                db.Remove(categoria);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });


        }
    }
}
