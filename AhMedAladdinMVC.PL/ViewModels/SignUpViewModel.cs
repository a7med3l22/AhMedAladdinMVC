using AhMedAladdinMVC.PL.ViewModels.Custom_Validation;
using System.ComponentModel.DataAnnotations;

namespace AhMedAladdinMVC.PL.ViewModels
{
	public class SignUpViewModel
	{
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string userName { get; set; }
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		public string email { get; set; }
		[DataType(DataType.Password)]
		public string password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(password),ErrorMessage ="Password Is Not Equals")]
		public string confirmPassword { get; set; }

		[Display(Name = "I agree to the terms")]
		[MustBeTrue]
		public bool IsAgree { get; set; }
	}
}
