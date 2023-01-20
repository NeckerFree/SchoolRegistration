using System.ComponentModel.DataAnnotations.Schema;

namespace SR.WebApp.Models
{
    public class ModelEvaluation
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IEnumerable<SR.WebApp.Models.DTOEvaluation> Evaluations { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
       
        public Guid IdCourse { get; set; }
    }
}
