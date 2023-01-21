using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using SR.DataAccess;
using SR.Models;
namespace SR.MinApi.EndPoints;

public static class CourseStudentEndpoints
{
    public static void MapCourseStudentEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/CourseStudent");

        group.MapGet("/", async (SchoolContext db) =>
        {
            return await db.CourseStudents.ToListAsync();
        })
        .WithName("GetAllCourseStudents");

        group.MapGet("/{id}", async Task<Results<Ok<CourseStudent>, NotFound>> (Guid id, SchoolContext db) =>
        {
            return await db.CourseStudents.FindAsync(id)
                is CourseStudent model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCourseStudentById");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (Guid id, CourseStudent courseStudent, SchoolContext db) =>
        {
            var foundModel = await db.CourseStudents.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(courseStudent);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateCourseStudent");

        group.MapPost("/", async (CourseStudent courseStudent, SchoolContext db) =>
        {
            db.CourseStudents.Add(courseStudent);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/CourseStudent/{courseStudent.Id}",courseStudent);
        })
        .WithName("CreateCourseStudent");

        group.MapDelete("/{id}", async Task<Results<Ok<CourseStudent>, NotFound>> (Guid id, SchoolContext db) =>
        {
            if (await db.CourseStudents.FindAsync(id) is CourseStudent courseStudent)
            {
                db.CourseStudents.Remove(courseStudent);
                await db.SaveChangesAsync();
                return TypedResults.Ok(courseStudent);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteCourseStudent");
    }
}
