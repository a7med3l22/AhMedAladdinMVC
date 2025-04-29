using AhMedAladdinMVC.BLL.IRepositories;
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
			return View();
		}
	}
}
