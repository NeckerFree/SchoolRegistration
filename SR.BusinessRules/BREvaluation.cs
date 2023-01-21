using SR.DataAccess;
using SR.Models.DTOs;
using SR.Models;
namespace SR.BusinessRules
{
    public class BREvaluation : IDisposable
    {
        private readonly SchoolContext _context;

        public BREvaluation(SchoolContext context)
        {
            _context = context;
        }

        public IQueryable<DTOEvaluation> GetEvaluations(Guid id, int selectedStart)
        {
            IQueryable<DTOEvaluation> evaluationsByCourse = (from ev in _context.Evaluations
                                                             join cs in _context.CourseStudents on ev.CourseStudentId equals cs.Id
                                                             join c in _context.Courses on cs.CourseId equals c.Id
                                                             join s in _context.Students on cs.StudentId equals s.Id
                                                             orderby ev.CreationDate
                                                             select new DTOEvaluation
                                                             {
                                                                 Id = id,
                                                                 CourseId = c.Id,
                                                                 Name = c.Name,
                                                                 StudentName = s.FullName,
                                                                 Active = c.Active,
                                                                 CreationDate = ev.CreationDate,
                                                                 Description = ev.Description,
                                                                 Grade = cs.Grade,
                                                                 Stars = ev.Stars
                                                             });
            if (id != Guid.Empty)
            {
                evaluationsByCourse = (from ec in evaluationsByCourse
                                       where ec.CourseId == id
                                       select ec);
            }
            if (selectedStart > 0)
            {
                evaluationsByCourse = (from ec in evaluationsByCourse
                                       where ec.Stars == selectedStart
                                       select ec);
            }
            return evaluationsByCourse;
        }
        public void Dispose()
        {
           GC.SuppressFinalize(this);
        }
    }
}
