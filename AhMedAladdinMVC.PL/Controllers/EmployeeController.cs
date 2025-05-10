using AhMedAladdinMVC.BLL.IRepositories;
using AhMedAladdinMVC.BLL.UnitOfWork;
using AhMedAladdinMVC.DAL.Models;
using AhMedAladdinMVC.PL.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;



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
		public IActionResult Index(String search)
		{
			IEnumerable<Employee> Emps;
			if (string.IsNullOrEmpty(search))			
				Emps = _unitOfWork.genericRepository<Employee>().GetAll();
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
		public IActionResult Create(EmployeeViewModel employeeVM)
		{
			if (ModelState.IsValid)
			{
				var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
				var count= _unitOfWork.genericRepository<Employee>().Add(employee);
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}
				SetDropDownLists();
				return View(employeeVM);
		}
		public IActionResult Edit(int? id,string action="Edit")
		{
			if (!id.HasValue)
				return BadRequest();
			
			var Employee= _unitOfWork.genericRepository<Employee>().GetById(id.Value);
			if(Employee == null)
				return NotFound();
			SetDropDownLists();
			var EmployeeVM=_mapper.Map<Employee, EmployeeViewModel>(Employee);
			return View(action, EmployeeVM);

		}
		[HttpPost]
		public IActionResult Edit(EmployeeViewModel employeeVM)
		{
			if (ModelState.IsValid)
			{
				var employee= _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
				var count = _unitOfWork.genericRepository<Employee>().Update(employee);
				if (count > 0)
					return RedirectToAction(nameof(Index));
			}
			SetDropDownLists();
			return View(employeeVM);
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
		public IActionResult Delete(EmployeeViewModel employeeVM)
		{
			var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
			_unitOfWork.genericRepository<Employee>().Delete(employee);
			return RedirectToAction(nameof(Index));

		}
	}
}
