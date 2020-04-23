using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
	public class StudentController : Controller
	{
		private readonly SchoolContext _context;

		public StudentController(SchoolContext context)
		{
			_context = context;
		}

		// GET: Student
		public async Task<IActionResult> Index(
			string sortOrder, string searchString,
			string currentFilter, int? pageNumber)
		{
			ViewData["CurrentSort"] = sortOrder;
			ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
			ViewData["CurrentFiler"] = searchString;
			var students = from s in _context.students select s;

			if (searchString != null)
			{
				pageNumber = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			//Searching
			if (!string.IsNullOrEmpty(searchString))
			{
				students = students
					.Where(s => s.lastName.Contains(searchString)
					|| s.firstName.Contains(searchString));
			}

			//Sorting
			switch (sortOrder)
			{
				case "name_desc":
					students = students.OrderByDescending(s => s.lastName);
					break;
				case "Date":
					students = students.OrderBy(s => s.dateEnrollment);
					break;
				case "date_desc":
					students = students.OrderByDescending(s => s.dateEnrollment);
					break;
				default:
					students = students.OrderBy(s => s.lastName);
					break;
			}

			int pageSize = 8;
			return View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
		}

		// GET: Student/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var student = await _context.students
				.Include(s => s.enrollments)
				.ThenInclude(e => e.course)
				.AsNoTracking()
				.FirstOrDefaultAsync(m => m.sudentId == id);
			if (student == null)
			{
				return NotFound();
			}

			return View(student);
		}

		// GET: Student/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Student/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("firstName,lastName,dateEnrollment")] Student student)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.Add(student);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
			}
			catch (DbUpdateException e)
			{
				ModelState.AddModelError("", "Unable to save changes.. smtg fcking error");
			}
			return View(student);
		}

		// GET: Student/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var student = await _context.students.FindAsync(id);
			if (student == null)
			{
				return NotFound();
			}
			return View(student);
		}

		// POST: Student/Edit/5
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
			var studentToUpdate = await _context.students
				.FirstOrDefaultAsync(student => student.sudentId == id);

			if (await TryUpdateModelAsync<Student>(
				studentToUpdate, "",
				s => s.firstName, s => s.lastName, s => s.dateEnrollment
				))
			{
				try
				{
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
				catch (DbUpdateException e)
				{
					ModelState.AddModelError("", "Unable to Save Change");
				}
			}

			return View(studentToUpdate);
		}

		// GET: Student/Delete/5
		public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
		{
			if (id == null)
			{
				return NotFound();
			}

			var student = await _context.students
				.AsNoTracking()
				.FirstOrDefaultAsync(m => m.sudentId == id);

			if (student == null)
			{
				return NotFound();
			}

			if (saveChangesError.GetValueOrDefault())
			{
				ViewData["Error Message"] = "Delete Failed ..  Try fcking again.";
			}

			return View(student);
		}

		// POST: Student/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var student = await _context.students.FindAsync(id);
			if (student == null)
			{
				return RedirectToAction(nameof(Index));
			}
			try
			{
				_context.students.Remove(student);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (DbUpdateException ex)
			{
				return RedirectToAction(nameof(Delete),
					new { id = id, saveChangesError = true });
			}
		}

		private bool StudentExists(int id)
		{
			return _context.students.Any(e => e.sudentId == id);
		}
	}
}
