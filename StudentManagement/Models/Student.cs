using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
	public class Student
	{
		[Key]
		public int sudentId { get; set; }

		// [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
		[Required]
		[StringLength(30, MinimumLength = 3)]
		[Display(Name = "First Name")]
		public string firstName { get; set; }

		[Required]
		[StringLength(20, MinimumLength = 3)]
		[Display(Name = "Last Name")]
		public string lastName { get; set; }

		[Display(Name = "Enrollment Date")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public DateTime dateEnrollment { get; set; }

		[Display(Name = "Full Name")]
		public string fullName {
			get {
				return firstName + " " + lastName; 
			}
		}

		public ICollection<Enrollment> enrollments { get; set; }
	}
}
