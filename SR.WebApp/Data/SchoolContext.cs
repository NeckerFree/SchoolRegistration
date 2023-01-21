//using Microsoft.EntityFrameworkCore;
//using SR.Models.DTOs;
//using SR.WebApp.Models;


//namespace SR.DataAccess;

//public partial class SchoolContext : DbContext
//{
//    public SchoolContext(DbContextOptions<SchoolContext> options)
//        : base(options)
//    {
//    }
//    public virtual DbSet<SR.Models.Course> Courses { get; set; } = default!;

//    public virtual DbSet<SR.Models.CourseStudent> CourseStudents { get; set; } = default!;

//    public virtual DbSet<SR.Models.Evaluation> Evaluations { get; set; } = default!;

//    public virtual DbSet<SR.Models.Student> Students { get; set; } = default!;

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<SR.Models.Course>(entity =>
//        {
//            entity.Property(e => e.Id).ValueGeneratedNever();
//        });

//        modelBuilder.Entity<SR.Models.CourseStudent>(entity =>
//        {
//            entity.Property(e => e.Id).ValueGeneratedNever();

//            entity.HasOne(d => d.Course).WithMany(p => p.CourseStudents).HasConstraintName("FK_CourseStudents_Courses");

//            entity.HasOne(d => d.Student).WithMany(p => p.CourseStudents).HasConstraintName("FK_CourseStudents_Students");
//        });

//        modelBuilder.Entity<SR.Models.Evaluation>(entity =>
//        {
//            entity.Property(e => e.Id).ValueGeneratedNever();

//            entity.HasOne(d => d.CourseStudent).WithMany(p => p.Evaluations).HasConstraintName("FK_Evaluations_CourseStudents");
//        });

//        modelBuilder.Entity<SR.Models.Student>(entity =>
//        {
//            entity.Property(e => e.Id).ValueGeneratedNever();
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

//    public DbSet<DTOEvaluation> DTOEvaluation { get; set; } = default!;
//}
