using AhMedAladdinMVC.BLL.IRepositories;
using AhMedAladdinMVC.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AhMedAladdinMVC.PL.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepository _departmentRepo;

		public DepartmentController(IDepartmentRepository departmentRepo)
		{
			_departmentRepo = departmentRepo;
		}
		public IActionResult Index()
		{
			var depatments= _departmentRepo.GetAll();
			return View(depatments);
		}
		
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Department department)
		{
			if(ModelState.IsValid)
			{
				var count = _departmentRepo.Add(department);
				if (count > 0)
				return RedirectToAction(nameof(Index));	
			}

			return View(department);
		}

		public IActionResult Details(int? id,string action= "Details")
		{
			if (!id.HasValue)
			{
				return BadRequest();
			}

			var department = _departmentRepo.GetById(id.Value);
			if (department is null)
			{
				return NotFound();
			}
			return View(action, department);
		}

		public IActionResult Edit(int id)
		{
			return Details(id, "Edit");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit([FromRoute]int Id, Department department)
		{
			if(department.Id != Id)
			{
				return BadRequest("Invalid input,  Please check your data.");
			}
			if (ModelState.IsValid)
			{
				var count = _departmentRepo.Update(department);
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}

			return View(department);
		}


		public IActionResult Delete(int id)
		{
			return Details(id, "Delete");
		}
		[HttpPost]
		public IActionResult Delete(Department department)
		{
		
				 _departmentRepo.Delete(department);
				
				return RedirectToAction(nameof(Index));
		
		}
	}
}

