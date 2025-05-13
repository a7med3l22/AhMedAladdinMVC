using AhMedAladdinMVC.DAL.Models;
using AhMedAladdinMVC.PL.ViewModels;
using AutoMapper;

namespace AhMedAladdinMVC.PL.Helpers.MappingProfile
{
	public class MappingDepartment : Profile
	{
		public MappingDepartment()
		{
			CreateMap<Department,DepartmentViewModel>().ReverseMap();
		}
	}
}
