using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
	public class Student : Person
	{

		[Display(Name = "Enrollment Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime dateEnrollment { get; set; }

		public ICollection<Enrollment> enrollments { get; set; }
	}
}
