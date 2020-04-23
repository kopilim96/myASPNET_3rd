using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
	public class OfficeAssignment
	{
		[Key]
		public int instructorId { get; set; }

		[StringLength(30, MinimumLength = 3)]
		[Display(Name = "Office Location")]
		public string location { get; set; }

		public Instructor Instructor;
	}
}
