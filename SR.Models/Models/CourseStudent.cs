using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SR.Models;

public partial class CourseStudent
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("course_id")]
    public Guid? CourseId { get; set; }

    [Column("student_id")]
    public Guid? StudentId { get; set; }

    [Column("grade")]
    public int? Grade { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("CourseStudents")]
    public virtual Course? Course { get; set; }

    [InverseProperty("CourseStudent")]
    public virtual ICollection<Evaluation> Evaluations { get; } = new List<Evaluation>();

    [ForeignKey("StudentId")]
    [InverseProperty("CourseStudents")]
    public virtual Student? Student { get; set; }
}
