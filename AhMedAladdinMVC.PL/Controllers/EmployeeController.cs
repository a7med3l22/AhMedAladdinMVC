using AhMedAladdinMVC.BLL.IRepositories;
using AhMedAladdinMVC.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;



namespace AhMedAladdinMVC.PL.Controllers
{
	public class EmployeeController : Controller
	{
		private void SetDropDownLists()
		{
			var genders = Enum.GetValues(typeof(Gender)).Cast<Gender>().Select(g => new { Value = (int)g, Text = g.ToString() });
			var EmpTypes = Enum.GetValues(typeof(EmpType)).Cast<EmpType>().Select(e => new { Value = (int)e, Text = e.ToString() });

			ViewBag.Genders = new SelectList(genders,"Value","Text");
			ViewBag.EmpTypes = new SelectList(EmpTypes,"Value","Text");
		}
		private readonly IEmployeeRepository _employeeRepo;

		public EmployeeController(IEmployeeRepository employeeRepo)
		{
			_employeeRepo = employeeRepo;
		}
		public IActionResult Index()
		{
			var Emps = _employeeRepo.GetAll();
			return View(Emps);
		}
		public IActionResult Create()
		{
			SetDropDownLists();
			return View();
		}
		[HttpPost]
		public IActionResult Create(Employee employee)
		{
			if (ModelState.IsValid)
			{
				var count= _employeeRepo.Add(employee);
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}
				SetDropDownLists();
				return View(employee);
		}
		public IActionResult Edit(int? id,string action="Edit")
		{
			if (!id.HasValue)
				return BadRequest();
			
			var Employee=_employeeRepo.GetById(id.Value);
			if(Employee == null)
				return NotFound();
			SetDropDownLists();
			return View(action,Employee);

		}
		[HttpPost]
		public IActionResult Edit(Employee employee)
		{
			if (ModelState.IsValid)
			{

				var count = _employeeRepo.Update(employee);
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}
			SetDropDownLists();
			return View(employee);
		}

		public IActionResult Details(int?id)
		{
			return Edit(id, "Details");
		}

		public IActionResult Delete(int id)
		{
			return Edit(id, "Delete");
		}

		[HttpPost]
		public IActionResult Delete(Employee employee)
		{

			_employeeRepo.Delete(employee);
			return RedirectToAction(nameof(Index));

		}
	}
}
