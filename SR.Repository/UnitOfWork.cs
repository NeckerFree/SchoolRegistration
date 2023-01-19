using SR.DataAccess;
using SR.Domain.Interfaces;

namespace SR.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly SchoolContext _dbContext;

        public IStudentRepository People { get; }

        public IEvaluationRepository Evaluationes { get; }

        public ICourseRepository Courses { get; }

        public UnitOfWork(SchoolContext dbContext,
                            IStudentRepository StudentRepository,
                            IEvaluationRepository EvaluationRepository,
                            ICourseRepository CourseRepository)
        {
            this._dbContext = dbContext;
            this.People = StudentRepository;
            this.Evaluationes = EvaluationRepository;
            this.Courses = CourseRepository;
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

        }


        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
