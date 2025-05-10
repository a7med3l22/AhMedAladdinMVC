using AhMedAladdinMVC.BLL.IRepositories;
using AhMedAladdinMVC.BLL.Repositories;
using AhMedAladdinMVC.BLL.UnitOfWork;

namespace AhMedAladdinMVC.PL.Extensions
{
	public static class MyAppExtensions
	{
		public static IServiceCollection AddMyAppExtensions(this IServiceCollection services)
		{
			//services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			//services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			services.AddScoped<IUnitOfWork,UnitOfWork> ();
			return services;

		}

	}
}
