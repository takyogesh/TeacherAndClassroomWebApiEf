
using Microsoft.EntityFrameworkCore;
using TeachersAndClassroomDll.Entities;

namespace TeachersAndClassroomDll
{
    public class TeacherAndClassroomDbContext : DbContext
    {
       public DbSet<Teacher>? Teachers { set; get; }
       public DbSet<Classroom>? Classrooms { set; get; }
        public TeacherAndClassroomDbContext()
        {
        }

        public TeacherAndClassroomDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-00LQG0A;Database=TeacherAndClassroomWebApiDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().Property(teacher => teacher.FirstName).IsRequired();
            modelBuilder.Entity<Teacher>().Property(teacher => teacher.LastName).IsRequired();
            modelBuilder.Entity<Teacher>().Property(teacher => teacher.Address).IsRequired();
            modelBuilder.Entity<Classroom>().Property(classroom => classroom.ClassName).IsRequired();
        }


    }
}