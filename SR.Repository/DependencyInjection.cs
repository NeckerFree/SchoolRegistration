using SR.DataAccess;
using SR.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SR.Repository
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SchoolContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEvaluationRepository, EvaluationRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<ICourseRepository, CourseRepository>();

            return services;
        }
    }
    
}
