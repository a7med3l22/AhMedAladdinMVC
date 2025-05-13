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
	public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
	{
		public EmployeeRepository(ApplicationDbContext context) : base(context) { }
		
		public IQueryable<Employee> GetEmpByAddress(string address) => _context.Set<Employee>().Where(emp=>string.Equals(emp.Address,address, StringComparison.OrdinalIgnoreCase));

		public IQueryable<Employee> GetEmpByName(string search)
		{
			return _context.Employees.Where(e => e.Name.Contains(search));
		}

		public override async Task<IEnumerable<Employee>> GetAllAsync()=>await _context.Employees.Include(e => e.Department).AsNoTracking().ToListAsync();
		
	}
}
