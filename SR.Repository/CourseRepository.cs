using SR.DataAccess;
using SR.Domain.Interfaces;
using SR.Models;

namespace SR.Repository
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(SchoolContext context) : base(context)
        {
        }
    }
    
}
