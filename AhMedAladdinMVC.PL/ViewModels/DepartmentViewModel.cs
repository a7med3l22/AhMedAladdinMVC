using AhMedAladdinMVC.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace AhMedAladdinMVC.PL.ViewModels
{
	public class DepartmentViewModel
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

	}
}
