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
	public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
	{
		public DepartmentRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
