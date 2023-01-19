using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SR.Models;

public partial class Evaluation
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("course_student_id")]
    public Guid? CourseStudentId { get; set; }

    [Column("stars")]
    public int? Stars { get; set; }

    [Column("description")]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("creation_date", TypeName = "date")]
    public DateTime? CreationDate { get; set; }

    [ForeignKey("CourseStudentId")]
    [InverseProperty("Evaluations")]
    public virtual CourseStudent? CourseStudent { get; set; }
}
