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
		public int Add(T entity)
		{
			 _context.Add(entity);
			return _context.SaveChanges();
		}

		public int Delete(T entity)
		{	
			_context.Remove(entity);
			return _context.SaveChanges();
		}

		public IEnumerable<T> GetAll()
		=> _context.Set<T>().AsNoTracking().ToList();


		public T? GetById(int id) => _context.Set<T>().Find(id);
		

		public int Update(T entity)
		{
			_context.Update(entity);
			return _context.SaveChanges();
		}
	}
}
