using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
	public class Department
	{
		public int departmentId { get; set; }

		[StringLength(40, MinimumLength = 3)]
		public string departName { get; set; }

		[DataType(DataType.Currency)]
		[Column(TypeName = "money")]
		public decimal budget { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(Name = "Build on")]
		public DateTime departStartDate { get; set; }

		public int? instructorId { get; set; }

		[Timestamp]
		public byte[] rowVersion { get; set; }

		public Instructor admin { get; set; }
		public ICollection<Course> courses { get; set; }
	}
}
