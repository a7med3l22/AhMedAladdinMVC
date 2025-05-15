using AhMedAladdinMVC.PL.ViewModels.Custom_Validation;
using System.ComponentModel.DataAnnotations;

namespace AhMedAladdinMVC.PL.ViewModels.AccountVM
{
	public class SignInViewModel
	{

		
		[Display(Name = "Email or Username")]
		public string EmailOrUsername { get; set; }

		[DataType(DataType.Password)]
		public string password { get; set; }

		[Display(Name = "Remember Me")]
		public bool RememberMe { get; set; }
	}
}
