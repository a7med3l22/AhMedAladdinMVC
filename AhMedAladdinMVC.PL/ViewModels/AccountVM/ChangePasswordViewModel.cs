using System.ComponentModel.DataAnnotations;

namespace AhMedAladdinMVC.PL.ViewModels.AccountVM
{
	public class ChangePasswordViewModel
	{
		[DataType(DataType.Password)]
		public string Newpassword { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Newpassword), ErrorMessage = "Password Is Not Equals")]
		public string confirmPassword { get; set; }
	}
}
