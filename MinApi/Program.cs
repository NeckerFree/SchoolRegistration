using Microsoft.EntityFrameworkCore;
using SR.DataAccess;
using SR.MinApi.EndPoints;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolConnection") ?? throw new InvalidOperationException("Connection string 'SchoolConnection' not found.")));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCourseEndpoints();

app.MapStudentEndpoints();

app.MapEvaluationEndpoints();

app.MapCourseStudentEndpoints();

app.MapDTOEvaluationEndpoints();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


//using SR.Repository;
//using SR.Services;
//using SR.Services.Interfaces;
//using Newtonsoft.Json;
//using SR.DataAccess;
//using Microsoft.EntityFrameworkCore;
//using SR.Models;

//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//var connectionString = builder.Configuration.GetConnectionString("SchoolConnection") ?? "";
////builder.Services.AddDIServices(connectionString);
//builder.Services.AddDbContext<SchoolContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddScoped<IStudentService, StudentService>();
//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.MapGet("/getAllStudents", async (IStudentService StudentService) =>
//{
//    return await StudentService.GetAllStudents();
//});
//app.MapGet("/getAdvancedStudents", (HttpContext http, IStudentService StudentService, [AsParameters] StudentParameters StudentParameters) =>
//{
//    if (!StudentParameters.ValidYearRange)
//    {
//        return Results.BadRequest("Max year of birth cannot be less than min year of birth");
//    }
//    var pagedStudents = StudentService.GetPagedStudents(StudentParameters);
//    var metadata = new
//    {
//        pagedStudents.TotalCount,
//        pagedStudents.PageSize,
//        pagedStudents.CurrentPage,
//        pagedStudents.TotalPages,
//        pagedStudents.HasNext,
//        pagedStudents.HasPrevious
//    };
//    http.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
//    return Results.Ok(pagedStudents);
//});


//app.Run();