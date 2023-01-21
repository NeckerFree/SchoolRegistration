using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SR.Models.DTOs
{
    public class DTOEvaluation
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("course_name")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Name { get; set; }
        public bool? Active { get; set; }
        [Column("grade")]
        public int? Grade { get; set; }
        [Column("stars")]
        [Range(1, 5)]
        public int? Stars { get; set; }
        [Column("description")]
        [Unicode(false)]
        public string? Description { get; set; }
        [Column("creation_date", TypeName = "date")]
        public DateTime? CreationDate { get; set; }
        public Guid CourseId { get;  set; }
        public string? StudentName { get; set; }
        
    }
}
