using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SR.Models;

public partial class Student
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column("last_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [Column("creation_date", TypeName = "date")]
    public DateTime? CreationDate { get; set; }

    [Column("active")]
    public bool? Active { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<CourseStudent> CourseStudents { get; } = new List<CourseStudent>();
}
