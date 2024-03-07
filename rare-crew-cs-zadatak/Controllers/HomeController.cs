using Microsoft.AspNetCore.Mvc;
using rare_crew_cs_zadatak.Interfaces;
using rare_crew_cs_zadatak.Models;
using System.Diagnostics;

namespace rare_crew_cs_zadatak.Controllers
{
	public class HomeController : Controller
	{
		private readonly IEmployeeService _employeeService;
		public HomeController(IEmployeeService employeeService)
		{
			_employeeService = employeeService;
		}

		public async Task<IActionResult> Index()
		{
			var employees = await _employeeService.GetEmployeesAsync();
			foreach (var employee in employees)
			{
				if (employee.EmployeeName is null) continue;

				int minutes = CalculateMinutesDifference(employee.StarTimeUtc, employee.EndTimeUtc);

				if (minutes < 0) continue;
				employee.MonthlyHours = minutes;
			}

			List<Employee> groupedEmployees = employees
				.Where(e => e.EmployeeName is not null)
				.GroupBy(e => e.EmployeeName)
				.Select(g => new Employee
				{
					EmployeeName = g.Key,
					MonthlyHours = g.Sum(e => e.MonthlyHours) / 60,
				})
				.OrderByDescending(e => e.MonthlyHours)
				.DistinctBy(e => e.EmployeeName)
				.ToList();

			return View(groupedEmployees);
		}

		private static int CalculateMinutesDifference(DateTime startDate, DateTime endDate)
		{
			TimeSpan timeDifference = endDate - startDate;
			return (int)timeDifference.TotalMinutes;
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
