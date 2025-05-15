using System.ComponentModel.DataAnnotations;

namespace AhMedAladdinMVC.PL.ViewModels.AccountVM
{
	public class ResetPasswordViewModel
	{
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		public string email { get; set; }
	}
}
