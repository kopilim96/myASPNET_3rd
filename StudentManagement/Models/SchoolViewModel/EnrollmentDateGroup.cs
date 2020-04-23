using System;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models.SchoolViewModel
{
	public class EnrollmentDateGroup
	{
		[DataType(DataType.Date)]
		public DateTime? EnrollmentDate { get; set; }
		public int studentCount { get; set; }
	}
}
