using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using SR.DataAccess;
using SR.Models;
namespace SR.MinApi.EndPoints;

public static class EvaluationEndpoints
{
    public static void MapEvaluationEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Evaluation");

        group.MapGet("/", async (SchoolContext db) =>
        {
            return await db.Evaluations.ToListAsync();
        })
        .WithName("GetAllEvaluations");

        group.MapGet("/{id}", async Task<Results<Ok<Evaluation>, NotFound>> (Guid id, SchoolContext db) =>
        {
            return await db.Evaluations.FindAsync(id)
                is Evaluation model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEvaluationById");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (Guid id, Evaluation evaluation, SchoolContext db) =>
        {
            var foundModel = await db.Evaluations.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(evaluation);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateEvaluation");

        group.MapPost("/", async (Evaluation evaluation, SchoolContext db) =>
        {
            db.Evaluations.Add(evaluation);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Evaluation/{evaluation.Id}",evaluation);
        })
        .WithName("CreateEvaluation");

        group.MapDelete("/{id}", async Task<Results<Ok<Evaluation>, NotFound>> (Guid id, SchoolContext db) =>
        {
            if (await db.Evaluations.FindAsync(id) is Evaluation evaluation)
            {
                db.Evaluations.Remove(evaluation);
                await db.SaveChangesAsync();
                return TypedResults.Ok(evaluation);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteEvaluation");
    }
}
