using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using SR.DataAccess;
using SR.Models;
namespace SR.MinApi.EndPoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Course");

        group.MapGet("/", async (SchoolContext db) =>
        {
            return await db.Courses.ToListAsync();
        })
        .WithName("GetAllCourses");

        group.MapGet("/{id}", async Task<Results<Ok<Course>, NotFound>> (Guid id, SchoolContext db) =>
        {
            return await db.Courses.FindAsync(id)
                is Course model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCourseById");

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (Guid id, Course course, SchoolContext db) =>
        {
            var foundModel = await db.Courses.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(course);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdateCourse");

        group.MapPost("/", async (Course course, SchoolContext db) =>
        {
            db.Courses.Add(course);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Course/{course.Id}",course);
        })
        .WithName("CreateCourse");

        group.MapDelete("/{id}", async Task<Results<Ok<Course>, NotFound>> (Guid id, SchoolContext db) =>
        {
            if (await db.Courses.FindAsync(id) is Course course)
            {
                db.Courses.Remove(course);
                await db.SaveChangesAsync();
                return TypedResults.Ok(course);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeleteCourse");
    }
}
