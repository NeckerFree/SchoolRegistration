using SR.DataAccess;
using SR.Domain.Interfaces;
using SR.Models;

namespace SR.Repository
{
       public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolContext context) : base(context)
        {
        }
    }
}
