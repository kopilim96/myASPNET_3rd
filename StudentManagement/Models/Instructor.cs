using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
	public class Instructor
	{
		[Required]
		[Display(Name = "First Name")]
		[StringLength(30, MinimumLength = 3)]
		public string firstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		[StringLength(30, MinimumLength = 3)]
		public string lastName { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-Mm-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Hire Date")]
		public DateTime hireDate { get; set; }

		[Display(Name = "Full Name"))]
		public string fullName
		{
			get
			{
				return firstName + " " + lastName;
			}
		}

		public ICollection<CourseAssignment> courseAssignments { get; set; }
		public OfficeAssignment officeAssignment { get; set; }
	}
}
