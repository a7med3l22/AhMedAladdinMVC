using AhMedAladdinMVC.BLL.IRepositories;
using AhMedAladdinMVC.BLL.Repositories;
using AhMedAladdinMVC.DAL.Data;
using AhMedAladdinMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhMedAladdinMVC.BLL.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _dbContext;

		 private readonly Dictionary<Type, object> _repository=new();

		public UnitOfWork(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		//عاوزة يطلبهم عند الطلب 

		public IGenericRepository<T> genericRepository<T>() where T: ModelBase
		{
			var key=typeof(T);
			if(!_repository.ContainsKey(key))
			{
				if(key== typeof(Employee))
				{
				_repository[key] = new EmployeeRepository(_dbContext);
				}
				else
				{
					_repository[key] = new GenericRepository<T>(_dbContext);
				}
			}

			return (IGenericRepository<T>)_repository[key];
		}


		public Task<int> CompleteAsync()
		{
			return _dbContext.SaveChangesAsync();
		}

		public ValueTask DisposeAsync()
		{
		return	_dbContext.DisposeAsync();
		}
	}
}
