using AhMedAladdinMVC.BLL.IRepositories;
using AhMedAladdinMVC.DAL.Models;
using AhMedAladdinMVC.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AhMedAladdinMVC.PL.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public DepartmentController(IMapper mapper,IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Index()
		{
			var depatments=await _unitOfWork.genericRepository<Department>().GetAllAsync();
			var departmentsVM= _mapper.Map<IEnumerable<Department>,IEnumerable<DepartmentViewModel>>(depatments);
			return View(departmentsVM);
		}
		
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
		{
			if(ModelState.IsValid)
			{
				var department=_mapper.Map<DepartmentViewModel,Department>(departmentVM);	
				 _unitOfWork.genericRepository<Department>().Add(department);
				var count =await _unitOfWork.CompleteAsync();
				if (count > 0)
				return RedirectToAction(nameof(Index));	
			}

			return View(departmentVM);
		}

		public async Task<IActionResult> Details(int? id,string action= "Details")
		{
			if (!id.HasValue)
			{
				return BadRequest();
			}

			var department =await _unitOfWork.genericRepository<Department>().GetById(id.Value);
			if (department is null)
			{
				return NotFound();
			}
			var departmentVM=_mapper.Map<Department,DepartmentViewModel>(department);
			return View(action, departmentVM);
		}

		public async Task<IActionResult> Edit(int id)
		{
			return await Details(id, "Edit");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute]int Id, DepartmentViewModel departmentVM)
		{
			if(departmentVM.Id != Id)
			{
				return BadRequest("Invalid input,  Please check your data.");
			}
			if (ModelState.IsValid)
			{
				var department=_mapper.Map<DepartmentViewModel,Department>(departmentVM);
				 _unitOfWork.genericRepository<Department>().Update(department);
				var count = await _unitOfWork.CompleteAsync();
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}

			return View(departmentVM);
		}


		public async Task<IActionResult> Delete(int id)
		{
			return await Details(id, "Delete");
		}
		[HttpPost]
		public async Task<IActionResult> Delete(DepartmentViewModel departmentVM)
		{
			var department=_mapper.Map<DepartmentViewModel,Department>(departmentVM);

			_unitOfWork.genericRepository<Department>().Delete(department);
			await _unitOfWork.CompleteAsync();

				return RedirectToAction(nameof(Index));
		
		}
	}
}

