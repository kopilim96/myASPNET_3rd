using System.Collections.Generic;

namespace StudentManagement.Models.SchoolViewModel
{
	public class InstructorIndexData
	{
		public IEnumerable<Instructor> instructors { get; set; }
		public IEnumerable<Course> courses { get; set; }
		public IEnumerable<Enrollment> enrollments { get; set; }
	}
}
