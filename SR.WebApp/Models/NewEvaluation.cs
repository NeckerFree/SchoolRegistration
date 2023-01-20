using SR.Models;

namespace SR.WebApp.Models
{
    public class NewEvaluation : Evaluation
    {
        public Guid? CourseId { get; set; }

        public Guid? StudentId { get; set; }

    }
}
