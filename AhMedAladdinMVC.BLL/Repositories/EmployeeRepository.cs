using AhMedAladdinMVC.BLL.IRepositories;
using AhMedAladdinMVC.DAL.Data;
using AhMedAladdinMVC.DAL.Models;
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
				


}
}
