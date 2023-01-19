using SR.DataAccess;
using SR.Domain.Interfaces;
using SR.Models;

namespace SR.Repository
{
    public class EvaluationRepository : GenericRepository<Evaluation>, IEvaluationRepository
    {
        public EvaluationRepository(SchoolContext context) : base(context)
        {
        }
    }
}
