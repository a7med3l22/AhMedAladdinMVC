using AhMedAladdinMVC.DAL.Models;
using AhMedAladdinMVC.PL.ViewModels.Custom_Validation;
using System.ComponentModel.DataAnnotations;

namespace AhMedAladdinMVC.PL.ViewModels
{

	public enum Gender
	{
		Male = 1,
		Female = 2
	}

	public enum EmpType
	{
		FullTime = 1,
		PartTime = 2
	}
	public class EmployeeViewModel
	{
		public int Id { get; set; }


		[MaxLength(50, ErrorMessage = "Max Length Is 50 Chars")]
		[MinLength(5, ErrorMessage = "Min Length Is 5 Chars")]
		public string Name { get; set; }


		[Range(22, 30)]
		public int? Age { get; set; }


		[RegularExpression(@"^\d+-[A-Za-z\s]+-[A-Za-z\s]+-[A-Za-z\s]+$",
		   ErrorMessage = "Address Must be like 123-Street-City-Country")]
		public string Address { get; set; }

		public decimal Salary { get; set; }

		[Display(Name = "Hiring Date")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime HiringDate { get; set; }
		public Gender Gender { get; set; }

		public EmpType EmpType { get; set; }

		[Phone]
		[DataType(DataType.PhoneNumber)]
		public string? PhoneNumber { get; set; }

		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		public string? Email { get; set; }


		[Display(Name = "Is Active")]
		public bool IsActive { get; set; }

		public int? DepartmentId { get; set; }

		public Department? Department { get; set; }
		public string? ImageName { get; set; }

		public IFormFile ? Image { get; set; }


	}
}
