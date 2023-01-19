using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using SR.DataAccess;
using SR.Models;
namespace SR.MinApi.EndPoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Student");

        group.MapGet("/", async (SchoolContext db) =>
        {
            return await db.Students.ToListAsync();
        })
        .WithName("GetAllStudents");

        group.MapGet("/{id}", async Task<Results<Ok<Student>, NotFound>> (Guid id, SchoolContext db) =>
        {
            return await db.Students.FindAsync(id)
                is Student model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetStudentById");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (Guid id, Student student, SchoolContext db) =>
        {
            var foundModel = await db.Students.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(student);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateStudent");

        group.MapPost("/", async (Student student, SchoolContext db) =>
        {
            db.Students.Add(student);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Student/{student.Id}",student);
        })
        .WithName("CreateStudent");

        group.MapDelete("/{id}", async Task<Results<Ok<Student>, NotFound>> (Guid id, SchoolContext db) =>
        {
            if (await db.Students.FindAsync(id) is Student student)
            {
                db.Students.Remove(student);
                await db.SaveChangesAsync();
                return TypedResults.Ok(student);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteStudent");
    }
}
