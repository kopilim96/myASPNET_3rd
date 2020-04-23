using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Models
{
	public class Course
	{

		//This is optional for generating primary key itself 
		//or we do it manual.. in this case
		//we generate the primary key ourself as set to None
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Display(Name = "Course ID")]
		public int courseId { get; set; }

		[StringLength(50)]
		public string courseTitle { get; set; }

		[Range(0, 5)]
		public int credit { get; set; }

		public int departmentId { get; set; }
		public Department department { get; set; }

		public ICollection<CourseAssignment> courseAssignments { get; set; }
		public ICollection<Enrollment> enrollments { get; set; }
	}
}
