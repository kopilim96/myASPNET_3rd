using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;
using StudentManagement.Models.SchoolViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
	public class InstructorController : Controller
	{
		private readonly SchoolContext _context;

		public InstructorController(SchoolContext context)
		{
			_context = context;
		}

		// GET: Instructor
		public async Task<IActionResult> Index(int? id, int? courseId)
		{
			var viewModel = new InstructorIndexData();
			viewModel.instructors = await _context.instructors
				.Include(i => i.officeAssignment)
				.Include(i => i.courseAssignments)
					.ThenInclude(i => i.course)
						.ThenInclude(i => i.enrollments)
							.ThenInclude(i => i.student)
				.Include(i => i.courseAssignments)
					.ThenInclude(i => i.course)
						.ThenInclude(i => i.department)
				.AsNoTracking()
				.OrderBy(i => i.lastName)
				.ToListAsync();

			if (id != null)
			{
				ViewData["instructorId"] = id.Value;
				Instructor instructor = viewModel.instructors
					.Where(i => i.id == id.Value).Single();
				viewModel.courses = instructor.courseAssignments
					.Select(s => s.course);
			}
			if (courseId != null)
			{
				ViewData["courseId"] = courseId.Value;

				viewModel.enrollments = viewModel.courses
					.Where(x => x.courseId == courseId).Single().enrollments;

				//Another method but it seems got error

				//var selectedCouse = viewModel.courses
				//.Where(x => x.courseId == courseId).Single();
				//await _context.Entry(selectedCouse).Collection(x => x.enrollments)
				//	.LoadAsync();
				//foreach (Enrollment enrollment in selectedCouse.enrollments) {
				//	await _context.Entry(enrollment).Reference(x => x.student)
				//		.LoadAsync();
				//}
				//viewModel.enrollments = selectedCouse.enrollments;
			}

			return View(viewModel);
		}

		// GET: Instructor/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var instructor = await _context.instructors
				.FirstOrDefaultAsync(m => m.id == id);
			if (instructor == null)
			{
				return NotFound();
			}

			return View(instructor);
		}

		// GET: Instructor/Create
		public IActionResult Create()
		{
			var instructor = new Instructor();
			instructor.courseAssignments = new List<CourseAssignment>();
			PopulateAssignedCourseData(instructor);
			return View();
		}

		// POST: Instructor/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([
			Bind("firstName,hireDate,lastName,officeAssignment")] 
		Instructor instructor, string[] selectedCourse)
		{
			if (selectedCourse != null) {
				instructor.courseAssignments = new List<CourseAssignment>();
				foreach (var course in selectedCourse) {
					var courseToAdd = new CourseAssignment
					{
						instructorId = instructor.id,
						courseId = int.Parse(course)
					};
					instructor.courseAssignments.Add(courseToAdd);
				}//End of forEach
			}//End of IF

			if (ModelState.IsValid) {
				_context.Add(instructor);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			PopulateAssignedCourseData(instructor);
			return View(instructor);
		}

		// GET: Instructor/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var instructor = await _context.instructors
				.Include(i => i.officeAssignment)
				.Include(i => i.courseAssignments)
					.ThenInclude(i => i.course)
				.AsNoTracking()
				.FirstOrDefaultAsync(m => m.id == id);

			if (instructor == null)
			{
				return NotFound();
			}
			PopulateAssignedCourseData(instructor);
			return View(instructor);
		}

		private void PopulateAssignedCourseData(Instructor instructor)
		{
			var allCourse = _context.courses;
			var instructorCourse = new HashSet<int>(
				instructor.courseAssignments
				.Select(c => c.courseId)
				);

			//This is view the data from the class we have declare
			var viewModel = new List<AssignedCourseData>();
			foreach (var item in allCourse)
			{
				viewModel.Add(new AssignedCourseData
				{
					courseaId = item.courseId,
					courseTitle = item.courseTitle,
					assigned = instructorCourse.Contains(item.courseId)
				});
			}

			ViewData["Course"] = viewModel;
		}


		// POST: Instructor/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, string[] selectedCourse)
		{
			if (id == null)
			{
				return NotFound();
			}

			var instructorToUpdate = await _context.instructors
				.Include(i => i.officeAssignment)
				.Include(i => i.courseAssignments)
					.ThenInclude(i => i.course)
				.FirstOrDefaultAsync(i => i.id == id);

			if (await TryUpdateModelAsync<Instructor>(
				instructorToUpdate, "",
				i => i.firstName,
				i => i.lastName,
				i => i.hireDate,
				i => i.officeAssignment))
			{
				if (string.IsNullOrWhiteSpace(instructorToUpdate.officeAssignment?.location))
				{
					instructorToUpdate.officeAssignment = null;
				}

				UpdateInstructorCourse(selectedCourse, instructorToUpdate);

				try
				{
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateException ex)
				{
					ModelState.AddModelError("", "Unable to Save Changes");
				}

				return RedirectToAction(nameof(Index));
			}

			UpdateInstructorCourse(selectedCourse, instructorToUpdate);
			PopulateAssignedCourseData(instructorToUpdate);

			return View(instructorToUpdate);
		}


		//Update Course Method
		private void UpdateInstructorCourse(string[] selectedCourse, Instructor instructorToUpdate)
		{
			//if choose ntg,
			//then return ntg
			if (selectedCourse == null)
			{
				instructorToUpdate.courseAssignments = new List<CourseAssignment>();
				return;
			}

			var selectedCourseHS = new HashSet<string>(selectedCourse);
			var instructorCourse = new HashSet<int>(
					instructorToUpdate.courseAssignments.Select(
							c => c.course.courseId
						)
				);

			foreach (var item in _context.courses)
			{
				if (selectedCourseHS.Contains(item.courseId.ToString()))
				{
					if (!instructorCourse.Contains(item.courseId))
					{
						//If the course are nt in the Instructor 
						//Then add course on it
						instructorToUpdate.courseAssignments.Add(
							new CourseAssignment
							{
								instructorId = instructorToUpdate.id,
								courseId = item.courseId
							}
							);
					} // End of 2nd IF
				}
				else
				{
					if (instructorCourse.Contains(item.courseId))
					{
						CourseAssignment courseRemove = instructorToUpdate.courseAssignments
							.FirstOrDefault(i => i.courseId == item.courseId);
						_context.Remove(courseRemove);
					}
				}
			}
		}

		// GET: Instructor/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var instructor = await _context.instructors
				.FirstOrDefaultAsync(m => m.id == id);
			if (instructor == null)
			{
				return NotFound();
			}

			return View(instructor);
		}

		// POST: Instructor/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			Instructor instructor = await _context.instructors
				.Include(i => i.courseAssignments)
				.SingleAsync(i => i.id == id);
			var departments = await _context.departments
				.Where(d => d.instructorId == id)
				.ToListAsync();

			departments.ForEach(d => d.instructorId = null);
			_context.instructors.Remove(instructor);

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool InstructorExists(int id)
		{
			return _context.instructors.Any(e => e.id == id);
		}
	}
}
