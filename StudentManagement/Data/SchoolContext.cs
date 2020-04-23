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


		//By normal if we using the EF and DBset will generate
		//the table for us but the name of the table is 
		//always in plural form... so if i like the table name in
		//singlular form this is the way to modify the behavior

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Course>().ToTable("Course");
			modelBuilder.Entity<Student>().ToTable("Student");
			modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
		}
	}
}
