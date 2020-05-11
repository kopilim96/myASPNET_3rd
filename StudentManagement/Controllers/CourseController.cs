using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
	public class CourseController : Controller
	{
		private readonly SchoolContext _context;

		public CourseController(SchoolContext context)
		{
			_context = context;
		}

		// GET: Course
		public async Task<IActionResult> Index()
		{
			var course = _context.courses
				.Include(c => c.department)
				.AsNoTracking();
			return View(await course.ToListAsync());
		}

		// GET: Course/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var course = await _context.courses
				.Include(c => c.department)
				.AsNoTracking()
				.FirstOrDefaultAsync(m => m.courseId == id);
			if (course == null)
			{
				return NotFound();
			}

			return View(course);
		}

		// GET: Course/Create
		public IActionResult Create()
		{
			PopulateDepartmentDropDownList();
			return View();
		}

		// POST: Course/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("courseId,credit,departmentId,courseTitle")] Course course)
		{
			if (ModelState.IsValid)
			{
				_context.Add(course);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			PopulateDepartmentDropDownList(course.departmentId);
			return View(course);
		}

		// GET: Course/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var course = await _context.courses
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.courseId == id);

			if (course == null)
			{
				return NotFound();
			}

			PopulateDepartmentDropDownList(course.departmentId);
			return View(course);
		}

		// POST: Course/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost, ActionName("Edit")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditPost(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var courseToUpdate = await _context.courses
				.FirstOrDefaultAsync(c => c.courseId == id);

			if (await TryUpdateModelAsync<Course>(courseToUpdate,
				"",
				c => c.credit,
				c => c.departmentId,
				c => c.courseTitle))
			{
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
			PopulateDepartmentDropDownList(courseToUpdate.departmentId);
			return View(courseToUpdate);
		}


		private void PopulateDepartmentDropDownList(object selectedDepartment = null)
		{
			var departmentQuery = from d in _context.departments orderby d.departName select d;

			ViewBag.DepartmentID = new SelectList(departmentQuery.AsNoTracking(), "departmentId", "departName", selectedDepartment);
		}


		// GET: Course/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var course = await _context.courses
				.Include(c => c.department)
				.AsNoTracking()
				.FirstOrDefaultAsync(m => m.courseId == id);
			if (course == null)
			{
				return NotFound();
			}

			return View(course);
		}

		// POST: Course/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var course = await _context.courses.FindAsync(id);
			_context.courses.Remove(course);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool CourseExists(int id)
		{
			return _context.courses.Any(e => e.courseId == id);
		}
	}
}
