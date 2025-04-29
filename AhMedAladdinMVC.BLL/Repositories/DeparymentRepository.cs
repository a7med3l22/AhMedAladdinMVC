using AhMedAladdinMVC.BLL.IRepositories;
using AhMedAladdinMVC.DAL.Data;
using AhMedAladdinMVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhMedAladdinMVC.BLL.Repositories
{
	public class DeparymentRepository : IDepartmentRepository
	{
		private readonly ApplicationDbContext _context;

		public DeparymentRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public int Add(Department entity)
		{
			_context.Departments.Add(entity);
			return _context.SaveChanges();
		}

		public int Delete(Department entity)
		{
			_context.Departments.Remove(entity);
			return _context.SaveChanges();
		}

		public IEnumerable<Department> GetAll() => _context.Departments.AsNoTracking().ToList();
		

		public Department GetById(int id) => _context.Departments.Find(id);
		


		public int Update(Department entity)
		{
			 _context.Departments.Update(entity);
			return _context.SaveChanges();
		}
	}
}
