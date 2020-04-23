using StudentManagement.Models;
using System;
using System.Linq;

namespace StudentManagement.Data
{
	public static class DbInitializer
	{
		public static void Initialize(SchoolContext context)
		{
			context.Database.EnsureCreated();

			if (context.students.Any())
			{
				return;
			}

			var students = new Student[]
			{
			new Student{firstName="Carson",lastName="Alexander",dateEnrollment=DateTime.Parse("2005-09-01")},
			new Student{firstName="Meredith",lastName="Alonso",dateEnrollment=DateTime.Parse("2002-09-01")},
			new Student{firstName="Arturo",lastName="Anand",dateEnrollment=DateTime.Parse("2003-09-01")},
			new Student{firstName="Gytis",lastName="Barzdukas",dateEnrollment=DateTime.Parse("2002-09-01")},
			new Student{firstName="Yan",lastName="Li",dateEnrollment=DateTime.Parse("2002-09-01")},
			new Student{firstName="Peggy",lastName="Justice",dateEnrollment=DateTime.Parse("2001-09-01")},
			new Student{firstName="Laura",lastName="Norman",dateEnrollment=DateTime.Parse("2003-09-01")},
			new Student{firstName="Nino",lastName="Olivetto",dateEnrollment=DateTime.Parse("2005-09-01")}
			};

			foreach (Student s in students)
			{
				context.students.Add(s);
			}
			context.SaveChanges();

			var courses = new Course[]
			{
			new Course{courseId=1050,courseTitle="Chemistry",credit=3},
			new Course{courseId=4022,courseTitle="Microeconomics",credit=3},
			new Course{courseId=4041,courseTitle="Macroeconomics",credit=3},
			new Course{courseId=1045,courseTitle="Calculus",credit=4},
			new Course{courseId=3141,courseTitle="Trigonometry",credit=4},
			new Course{courseId=2021,courseTitle="Composition",credit=3},
			new Course{courseId=2042,courseTitle="Literature",credit=4}
			};

			foreach (Course c in courses)
			{
				context.courses.Add(c);
			}
			context.SaveChanges();

			var enrollments = new Enrollment[]
			{
			new Enrollment{studentId=1,courseId=1050,grade=Enrollment.Grade.A},
			new Enrollment{studentId=1,courseId=4022,grade=Enrollment.Grade.C},
			new Enrollment{studentId=1,courseId=4041,grade=Enrollment.Grade.B},
			new Enrollment{studentId=2,courseId=1045,grade=Enrollment.Grade.B},
			new Enrollment{studentId=2,courseId=3141,grade=Enrollment.Grade.F},
			new Enrollment{studentId=2,courseId=2021,grade=Enrollment.Grade.F},
			new Enrollment{studentId=3,courseId=1050},
			new Enrollment{studentId=4,courseId=1050},
			new Enrollment{studentId=4,courseId=4022,grade=Enrollment.Grade.F},
			new Enrollment{studentId=5,courseId=4041,grade=Enrollment.Grade.C},
			new Enrollment{studentId=6,courseId=1045},
			new Enrollment{studentId=7,courseId=3141,grade=Enrollment.Grade.A},
			};
			foreach (Enrollment e in enrollments)
			{
				context.enrollments.Add(e);
			}
			context.SaveChanges();
		}
	}
}
