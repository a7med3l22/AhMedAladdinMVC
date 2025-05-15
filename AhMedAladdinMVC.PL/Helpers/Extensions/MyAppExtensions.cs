using AhMedAladdinMVC.BLL.IRepositories;
using AhMedAladdinMVC.BLL.Repositories;
using AhMedAladdinMVC.BLL.UnitOfWork;
using AhMedAladdinMVC.PL.Services.EmailSender;

namespace AhMedAladdinMVC.PL.Helpers.Extensions
{
	public static class MyAppExtensions
	{
		public static IServiceCollection AddMyAppExtensions(this IServiceCollection services)
		{
			//services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			//services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			services.AddScoped<IEmailSender, EmailSender>();
			services.AddScoped<IUnitOfWork,UnitOfWork> ();
			return services;

		}

	}
}
