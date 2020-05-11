using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
	public class Instructor : Person
	{
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Hire Date")]
		public DateTime hireDate { get; set; }

		public ICollection<CourseAssignment> courseAssignments { get; set; }
		public OfficeAssignment officeAssignment { get; set; }
	}
}
