using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StudentManagement.Data;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly SchoolContext _context;

		public DepartmentController(SchoolContext context)
		{
			_context = context;
		}

		// GET: Department
		public async Task<IActionResult> Index()
		{
			var schoolContext = _context.departments.Include(d => d.admin);
			return View(await schoolContext.ToListAsync());
		}

		// GET: Department/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var department = await _context.departments
				.Include(d => d.admin)
				.FirstOrDefaultAsync(m => m.departmentId == id);
			if (department == null)
			{
				return NotFound();
			}

			return View(department);
		}

		// GET: Department/Create
		public IActionResult Create()
		{
			ViewData["instructorId"] = new SelectList(_context.instructors, "instructorId", "fullName");
			return View();
		}

		// POST: Department/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("departmentId,departName,budget,departStartDate,instructorId,rowVersion")] Department department)
		{
			if (ModelState.IsValid)
			{
				_context.Add(department);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["instructorId"] = new SelectList(_context.instructors, "instructorId", "fullName", department.instructorId);
			return View(department);
		}

		// GET: Department/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var department = await _context.departments
				.Include(i => i.admin)
				.AsNoTracking()
				.FirstOrDefaultAsync(m => m.departmentId == id);

			if (department == null)
			{
				return NotFound();
			}
			ViewData["instructorId"] = new SelectList(_context.instructors, "instructorId", "fullName", department.instructorId);
			return View(department);
		}

		// POST: Department/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int? id, byte[] rowVersion)
		{
			if (id == null)
			{
				return NotFound();
			}

			var departmentToUpdate = await _context.departments
				.Include(i => i.admin)
				.FirstOrDefaultAsync(m => m.departmentId == id);

			if (departmentToUpdate == null)
			{
				Department deleteDepart = new Department();
				await TryUpdateModelAsync(departmentToUpdate);
				ModelState.AddModelError(string.Empty, "Unable to save changes");
				ViewData["instructorId"] = new SelectList(_context.instructors,
					"instructorId", "fullName"
					, deleteDepart.instructorId);
				return View(deleteDepart);
			}

			_context.Entry(departmentToUpdate).Property("rowVersion").OriginalValue = rowVersion;

			if (await TryUpdateModelAsync<Department>(departmentToUpdate,
				"",
				s => s.departName, s => s.departStartDate,
				s => s.budget, s => s.instructorId))
			{
				try
				{

					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));

				}
				catch (DbUpdateConcurrencyException ex)
				{

					var exceptionEntry = ex.Entries.Single();
					var clientValue = (Department)exceptionEntry.Entity;
					var databaseEntry = exceptionEntry.GetDatabaseValues();
					if (databaseEntry == null)
					{
						ModelState.AddModelError(string.Empty,
							"Unable to save changes");
					}
					else
					{
						var databaseValue = (Department)databaseEntry.ToObject();
						if (databaseValue.departName != clientValue.departName)
						{
							ModelState.AddModelError("departName", $"Current Value: {databaseValue.departName}");
						}
						if (databaseValue.budget != clientValue.budget)
						{
							ModelState.AddModelError("budget", $"Current Value: {databaseValue.budget}");
						}
						if (databaseValue.departStartDate != clientValue.departStartDate)
						{
							ModelState.AddModelError("departStartDate", $"Current Value: {databaseValue.departStartDate}");
						}
						if (databaseValue.instructorId != clientValue.instructorId)
						{
							Instructor databaseInstructor = await _context.instructors
								.FirstOrDefaultAsync(i => i.id == databaseValue.instructorId);
							ModelState.AddModelError("instructorId", $"Current Value: {databaseValue.instructorId}");
						}

						ModelState.AddModelError(string.Empty,
							"Record you attempted to edit was modified by someone, pls try again.");
						departmentToUpdate.rowVersion = (byte[])databaseValue.rowVersion;
						ModelState.Remove("rowVersion");
					}

				}
			}
			ViewData["instructorId"] = new SelectList(_context.instructors,
				"instructorId", "fullName",
				departmentToUpdate.instructorId);
			return View(departmentToUpdate);
		}

		// GET: Department/Delete/5
		public async Task<IActionResult> Delete(int? id, bool? concurrencyError)
		{
			if (id == null)
			{
				return NotFound();
			}

			var department = await _context.departments
				.Include(d => d.admin)
				.AsNoTracking()
				.FirstOrDefaultAsync(m => m.departmentId == id);

			if (department == null)
			{
				if (concurrencyError.GetValueOrDefault())
				{
					return RedirectToAction(nameof(Index));
				}

				return NotFound();
			}

			if (concurrencyError.GetValueOrDefault())
			{
				ViewData["ConcurrencyErrorMsg"] = "Record you attempted to delete was modified by someone.. pls try again later..";
			}

			return View(department);
		}

		// POST: Department/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(Department department)
		{
			try
			{
				if (await _context.departments.AnyAsync(
					m => m.departmentId == department.departmentId))
				{
					_context.departments.Remove(department);
					await _context.SaveChangesAsync();
				}

				return RedirectToAction(nameof(Index));

			}
			catch (DbUpdateConcurrencyException ex)
			{
				return RedirectToAction(nameof(Delete), new
				{
					concurrencyError = true,
					id = department.departmentId
				});
			}
		}

		private bool DepartmentExists(int id)
		{
			return _context.departments.Any(e => e.departmentId == id);
		}
	}
}
