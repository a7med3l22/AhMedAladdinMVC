namespace AhMedAladdinMVC.PL.Services.EmailSender
{
	public interface IEmailSender
	{
	 	Task SendEmailAsync(string toEmail, string subject, string message);
	}
}
