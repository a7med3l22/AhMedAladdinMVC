using AhMedAladdinMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhMedAladdinMVC.BLL.IRepositories
{
	public interface IUnitOfWork:IAsyncDisposable
	{
		public IGenericRepository<T> genericRepository<T>() where T : ModelBase;
		Task<int> CompleteAsync();
	}
}
