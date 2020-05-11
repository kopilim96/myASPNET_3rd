using StudentManagement.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
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



			var instructors = new Instructor[]
			{
				new Instructor {firstName = "Kim",lastName = "Abercrombie",hireDate = DateTime.Parse("1995-03-11") },
				new Instructor {firstName = "Fadi",lastName = "Fakhouri",hireDate = DateTime.Parse("2002-07-06") },
				new Instructor {firstName = "Roger",lastName = "Harui",hireDate = DateTime.Parse("1998-07-01") },
				new Instructor {firstName = "Candace",lastName = "Kapoor",hireDate = DateTime.Parse("2001-01-15") },
				new Instructor {firstName = "Roger",lastName = "Zheng",hireDate = DateTime.Parse("2004-02-12") }
			};

			foreach (Instructor i in instructors)
			{
				context.instructors.Add(i);
			}
			context.SaveChanges();



			var departments = new Department[]
			{
				new Department {departName = "English",budget = 350000,
					departStartDate = DateTime.Parse("2007-09-01"),
					instructorId  = instructors.Single( i => i.lastName == "Abercrombie").id },
				new Department {departName = "Mathematics",budget = 100000,
					departStartDate = DateTime.Parse("2007-09-01"),
					instructorId  = instructors.Single( i => i.lastName == "Fakhouri").id },
				new Department {departName = "Engineering", budget = 350000,
					departStartDate = DateTime.Parse("2007-09-01"),
					instructorId  = instructors.Single( i => i.lastName == "Harui").id },
				new Department {departName = "Economics",   budget = 100000,
					departStartDate = DateTime.Parse("2007-09-01"),
					instructorId  = instructors.Single( i => i.lastName == "Kapoor").id }
			};

			foreach (Department d in departments)
			{
				context.departments.Add(d);
			}
			context.SaveChanges();



			var courses = new Course[]
			{
			new Course{
				courseId=1050,courseTitle="Chemistry",credit=3,
				departmentId = departments.Single(s => s.departName == "Engineering").departmentId
			},
			new Course{
				courseId=4022,courseTitle="Microeconomics",credit=3,
				departmentId = departments.Single(s => s.departName == "Economics").departmentId
			},
			new Course{
				courseId=4041,courseTitle="Macroeconomics",credit=3,
				departmentId = departments.Single(s => s.departName == "Economics").departmentId
			},
			new Course{
				courseId=1045,courseTitle="Calculus",credit=4,
				departmentId = departments.Single(s => s.departName == "Mathematics").departmentId
			},
			new Course{
				courseId=3141,courseTitle="Trigonometry",credit=4,
				departmentId = departments.Single(s => s.departName == "Mathematics").departmentId
			},
			new Course{
				courseId=2021,courseTitle="Composition",credit=3,
				departmentId = departments.Single(s => s.departName == "English").departmentId
			},
			new Course{
				courseId=2042,courseTitle="Literature",credit=4,
				departmentId = departments.Single(s => s.departName == "English").departmentId
			}
			};

			foreach (Course c in courses)
			{
				context.courses.Add(c);
			}
			context.SaveChanges();

			var officeAssignments = new OfficeAssignment[]
		   {
				new OfficeAssignment {
					instructorId = instructors.Single( i => i.lastName == "Fakhouri").id,
					location = "Smith 17" },
				new OfficeAssignment {
					instructorId = instructors.Single( i => i.lastName == "Harui").id,
					location = "Gowan 27" },
				new OfficeAssignment {
					instructorId = instructors.Single( i => i.lastName == "Kapoor").id,
					location = "Thompson 304" },
		   };

			foreach (OfficeAssignment o in officeAssignments)
			{
				context.officeAssignments.Add(o);
			}
			context.SaveChanges();


			var courseInstructors = new CourseAssignment[]
			{
				new CourseAssignment {
					courseId = courses.Single(c => c.courseTitle == "Chemistry" ).courseId,
					instructorId = instructors.Single(i => i.lastName == "Kapoor").id
					},
				new CourseAssignment {
					courseId = courses.Single(c => c.courseTitle == "Chemistry" ).courseId,
					instructorId = instructors.Single(i => i.lastName == "Harui").id
					},
				new CourseAssignment {
					courseId = courses.Single(c => c.courseTitle == "Microeconomics" ).courseId,
					instructorId = instructors.Single(i => i.lastName == "Zheng").id
					},
				new CourseAssignment {
					courseId = courses.Single(c => c.courseTitle == "Macroeconomics" ).courseId,
					instructorId = instructors.Single(i => i.lastName == "Zheng").id
					},
				new CourseAssignment {
					courseId = courses.Single(c => c.courseTitle == "Calculus" ).courseId,
					instructorId = instructors.Single(i => i.lastName == "Fakhouri").id
					},
				new CourseAssignment {
					courseId = courses.Single(c => c.courseTitle == "Trigonometry" ).courseId,
					instructorId = instructors.Single(i => i.lastName == "Harui").id
					},
				new CourseAssignment {
					courseId = courses.Single(c => c.courseTitle == "Composition" ).courseId,
					instructorId = instructors.Single(i => i.lastName == "Abercrombie").id
					},
				new CourseAssignment {
					courseId = courses.Single(c => c.courseTitle == "Literature" ).courseId,
					instructorId = instructors.Single(i => i.lastName == "Abercrombie").id
					},
			};

			foreach (CourseAssignment ci in courseInstructors)
			{
				context.courseAssignments.Add(ci);
			}
			context.SaveChanges();


			var enrollments = new Enrollment[]
			{
				new Enrollment {
					studentId = students.Single(s => s.lastName == "Alexander").id,
					courseId = courses.Single(c => c.courseTitle == "Chemistry" ).courseId,
					grade = Enrollment.Grade.A
				},
				new Enrollment {
					studentId = students.Single(s => s.lastName == "Alexander").id,
					courseId = courses.Single(c => c.courseTitle == "Microeconomics" ).courseId,
					grade = Enrollment.Grade.C
				},
				new Enrollment {
					studentId = students.Single(s => s.lastName == "Alexander").id,
					courseId = courses.Single(c => c.courseTitle == "Macroeconomics" ).courseId,
					grade = Enrollment.Grade.B
				},
				new Enrollment {
					studentId = students.Single(s => s.lastName == "Alonso").id,
					courseId = courses.Single(c => c.courseTitle == "Calculus" ).courseId,
					grade = Enrollment.Grade.B
				},
				new Enrollment {
					studentId = students.Single(s => s.lastName == "Alonso").id,
					courseId = courses.Single(c => c.courseTitle == "Trigonometry" ).courseId,
					grade = Enrollment.Grade.B
				},
				new Enrollment {
					studentId = students.Single(s => s.lastName == "Alonso").id,
					courseId = courses.Single(c => c.courseTitle == "Composition" ).courseId,
					grade = Enrollment.Grade.B
				},
				new Enrollment {
					studentId = students.Single(s => s.lastName == "Anand").id,
					courseId = courses.Single(c => c.courseTitle == "Chemistry" ).courseId,
					grade = Enrollment.Grade.D
				},
				new Enrollment {
					studentId = students.Single(s => s.lastName == "Anand").id,
					courseId = courses.Single(c => c.courseTitle == "Microeconomics").courseId,
					grade = Enrollment.Grade.D
				},
				new Enrollment {
					studentId = students.Single(s => s.lastName == "Barzdukas").id,
					courseId = courses.Single(c => c.courseTitle == "Chemistry").courseId,
					grade = Enrollment.Grade.B
				},
				new Enrollment {
					studentId = students.Single(s => s.lastName == "Li").id,
					courseId = courses.Single(c => c.courseTitle == "Composition").courseId,
					grade = Enrollment.Grade.F
				},
				new Enrollment {
					studentId = students.Single(s => s.lastName == "Justice").id,
					courseId = courses.Single(c => c.courseTitle == "Literature").courseId,
					grade = Enrollment.Grade.B
				}
			};

			foreach (Enrollment e in enrollments)
			{
				var enrollmentDb = context.enrollments.Where(
						s => 
						s.student.id == e.studentId &&
						s.course.courseId == e.courseId
					).SingleOrDefault();

				if (enrollmentDb == null)
				{
					context.enrollments.Add(e);
				}

			}
			context.SaveChanges();

		}//End of class
	}
}
