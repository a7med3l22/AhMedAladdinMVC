using AhMedAladdinMVC.BLL.IRepositories;
using AhMedAladdinMVC.DAL.Models;
using AhMedAladdinMVC.PL.Helpers.Document;
using AhMedAladdinMVC.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace AhMedAladdinMVC.PL.Controllers
{
	
	public class EmployeeController : Controller
	{
		private void SetDropDownLists()
		{
			var genders = Enum.GetValues(typeof(ViewModels.Gender)).Cast<ViewModels.Gender>().Select(g => new { Value = (int)g, Text = g.ToString() });
			var EmpTypes = Enum.GetValues(typeof(ViewModels.EmpType)).Cast<ViewModels.EmpType>().Select(e => new { Value = (int)e, Text = e.ToString() });

			ViewBag.Genders = new SelectList(genders,"Value","Text");
			ViewBag.EmpTypes = new SelectList(EmpTypes,"Value","Text");
		}

		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public EmployeeController(IMapper mapper,IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}
		public async Task<IActionResult> Index(String search)
		{
			IEnumerable<Employee> Emps;
			if (string.IsNullOrEmpty(search))			
				Emps =await _unitOfWork.genericRepository<Employee>().GetAllAsync();
			else
			    Emps = ((IEmployeeRepository)_unitOfWork.genericRepository<Employee>()).GetEmpByName(search);
			
			var empView = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Emps);
			return View(empView);
		}
		public IActionResult Create()
		{
			SetDropDownLists();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
		{
			if (ModelState.IsValid)
			{
				if(employeeVM.Image is not null)
					employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");

				var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
				 _unitOfWork.genericRepository<Employee>().Add(employee);
				var count =await _unitOfWork.CompleteAsync();
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}
				SetDropDownLists();
				return View(employeeVM);
		}
		public async Task<IActionResult> Edit(int? id,string action="Edit")
		{
			if (!id.HasValue)
				return BadRequest();
			
			var Employee= await _unitOfWork.genericRepository<Employee>().GetById(id.Value);
			if(Employee == null)
				return NotFound();
			SetDropDownLists();
			var EmployeeVM=_mapper.Map<Employee, EmployeeViewModel>(Employee);
			return View(action, EmployeeVM);

		}
		[HttpPost]
		public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, bool RemoveImage = false)
		{
			
			if (ModelState.IsValid)
			{
				if (RemoveImage)
				{
					if (!string.IsNullOrEmpty(employeeVM.ImageName))
						DocumentSetting.DeleteFile("Images", employeeVM.ImageName);

					employeeVM.ImageName = null;
				}

				if (employeeVM.Image is not null)
				{
					if (!string.IsNullOrEmpty(employeeVM.ImageName))
						DocumentSetting.DeleteFile("Images", employeeVM.ImageName);
					employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");
				}
				
				var employee= _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
				  _unitOfWork.genericRepository<Employee>().Update(employee);
				  var count =await _unitOfWork.CompleteAsync();

				if (count > 0)
					return RedirectToAction(nameof(Index));
			}
			SetDropDownLists();
			return View(employeeVM);
		}

		public async Task<IActionResult> Details(int?id)
		{ 
			return await Edit(id, "Details");
		}

		public async Task<IActionResult> Delete(int id)
		{
			return await Edit(id, "Delete");
		}

		[HttpPost]
		public async Task<IActionResult> Delete(EmployeeViewModel employeeVM)
		{
			if (!string.IsNullOrEmpty(employeeVM.ImageName))
				DocumentSetting.DeleteFile("Images", employeeVM.ImageName);

			var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
			_unitOfWork.genericRepository<Employee>().Delete(employee);
			 await _unitOfWork.CompleteAsync();
			return RedirectToAction(nameof(Index));

		}
	}
}
