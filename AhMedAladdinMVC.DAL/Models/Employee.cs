using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhMedAladdinMVC.DAL.Models
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

	public class Employee:ModelBase
	{


		[MaxLength(50,ErrorMessage ="Max Length Is 50 Chars")]
		[MinLength(5,ErrorMessage = "Min Length Is 5 Chars")]
		public string Name { get; set; }


		[Range(22,30)]
		public int? Age { get; set; }


		[RegularExpression(@"^\d+-[A-Za-z\s]+-[A-Za-z\s]+-[A-Za-z\s]+$",
		   ErrorMessage = "Address Must be like 123-Street-City-Country")]
		public string Address { get; set; }


		
		public decimal Salary { get; set; }



		[Display(Name = "Hiring Date")]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime HiringDate { get; set; }

		[Range(1, 2, ErrorMessage = "Please select Gender")]
		public Gender Gender { get; set; }

		[Range(1, 2, ErrorMessage = "Please select employee type")]
		public EmpType EmpType { get; set; }



		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime CreationDate { get; set; }= DateTime.Now;


		public bool IsDeleted { get; set; }=false;


		[Phone]
		[DataType(DataType.PhoneNumber)]
		public string ? PhoneNumber { get; set; }


		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		public string ? Email { get; set; }



		[Display(Name = "Is Active")]
		public bool IsActive { get; set; }


	}
}
