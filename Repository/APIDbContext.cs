using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Entity;

namespace StudentManagementSystem.Repository
{
    public class APIDbContext:DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> context) : base(context)
        {

        }
        public DbSet<Student> Student { get; set; }
        public DbSet<Subject> subject { get; set; }
        public DbSet<StudentSubject> StudentSubject { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Subject>()
                .HasKey(y => y.SubjectId);

            modelBuilder.Entity<StudentSubject>()
                    .HasKey(ss => new { ss.StudentId, ss.SubjectId }); // Composite key

            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSubject)
                .HasForeignKey(ss => ss.StudentId);

            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.StudentSubject)
                .HasForeignKey(ss => ss.SubjectId);

            //Unique validation for EmailId
            modelBuilder.Entity<Student>()
                .HasIndex(x => x.EmailId)
                .IsUnique();
              
        }
    }
}
