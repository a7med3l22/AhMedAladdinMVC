using AhMedAladdinMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhMedAladdinMVC.BLL.IRepositories
{
	public interface IGenericRepository<T> where T : ModelBase 
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T?> GetById(int id);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);


	}
	
}
