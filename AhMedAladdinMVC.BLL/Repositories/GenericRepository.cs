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
	public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
	{
		private protected readonly ApplicationDbContext _context;

		public GenericRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public void Add(T entity)
		{
			 _context.Add(entity);
		}

		public void Delete(T entity)
		{	
			_context.Remove(entity);
		}

		public virtual async Task<IEnumerable<T>> GetAllAsync()=>await _context.Set<T>().AsNoTracking().ToListAsync();
		

		public async Task<T?> GetById(int id) =>await _context.Set<T>().FindAsync(id);
		

		public void Update(T entity)
		{
			_context.Update(entity);
		}
	}
}
