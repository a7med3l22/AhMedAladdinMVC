using AhMedAladdinMVC.DAL.Models;
using AhMedAladdinMVC.PL.ViewModels;
using AutoMapper;

namespace AhMedAladdinMVC.PL.MappingProfile
{
	public class MappingEmployee : Profile
	{
		public MappingEmployee()
		{
			CreateMap<Employee,EmployeeViewModel>().ReverseMap();
		}

	}
}
