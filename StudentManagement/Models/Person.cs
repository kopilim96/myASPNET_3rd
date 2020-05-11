using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
	public class Person
	{
		public int id { get; set; }

		[Required]
		[StringLength(40)]
		[Display(Name = "Last Name")]
		public string lastName { get; set; }

		[Required]
		[StringLength(40, ErrorMessage = "Should not more than 40 Character")]
		[Column("fisrtName")]
		[Display(Name = "firstName")]
		public string firstName { get; set; }

		[Display(Name = "fullName")]
		public string fullName
		{
			get { return firstName + " " + lastName; }
		}
	}
}
