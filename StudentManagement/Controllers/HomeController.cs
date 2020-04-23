﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentManagement.Data;
using StudentManagement.Models;
using StudentManagement.Models.SchoolViewModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Controllers
{
	public class HomeController : Controller
	{
		private readonly SchoolContext _context;

		public HomeController(SchoolContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public async Task<ActionResult> About()
		{
			IQueryable<EnrollmentDateGroup> data = from student in _context.students
												   group student by student.dateEnrollment
												   into dateGroup
												   select new EnrollmentDateGroup()
												   {
													   EnrollmentDate = dateGroup.Key,
													   studentCount = dateGroup.Count()
												   };
			return View(await data.AsNoTracking().ToListAsync());
		}
	}
}
