using Microsoft.EntityFrameworkCore;
using StudentManagement.Models;

namespace StudentManagement.Data
{
	public class SchoolContext : DbContext
	{
		public SchoolContext(DbContextOptions<SchoolContext> options)
			: base(options)
		{

		}

		public DbSet<Course> courses { get; set; }
		public DbSet<Enrollment> enrollments { get; set; }
		public DbSet<Student> students { get; set; }
		public DbSet<Department> departments { get; set; }
		public DbSet<Instructor> instructors { get; set; }
		public DbSet<OfficeAssignment> officeAssignments { get; set; }
		public DbSet<CourseAssignment> courseAssignments { get; set; }
		
		
		//Parent Class Person extend Instructor and Student
		public DbSet<Person> people { get; set; }


		//By normal if we using the EF and DBset will generate
		//the table for us but the name of the table is 
		//always in plural form... so if i like the table name in
		//singlular form this is the way to modify the behavior

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Course>().ToTable("Course");
			modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
			modelBuilder.Entity<Department>().ToTable("Department");
			modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
			modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");
			modelBuilder.Entity<Person>().ToTable("Person");

			modelBuilder.Entity<CourseAssignment>().HasKey(c =>
				new
				{
					c.courseId,
					c.instructorId
				});
		}
	}
}
