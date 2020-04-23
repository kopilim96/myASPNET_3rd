using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
	public class Enrollment
	{
		public enum Grade
		{
			A, B, C, D, F
		}

		public int enrollmentId { get; set; }
		public int studentId { get; set; }
		public int courseId { get; set; }

		[DisplayFormat(NullDisplayText = "Grade Pending")]
		public Grade? grade { get; set; }
		public Course course { get; set; }
		public Student student { get; set; }
	}
}
