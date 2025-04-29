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
		[HttpGet]
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
	}
}
