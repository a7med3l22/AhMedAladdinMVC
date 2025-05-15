using AhMedAladdinMVC.Controllers;
using AhMedAladdinMVC.DAL.Models;
using AhMedAladdinMVC.PL.Services.EmailSender;
using AhMedAladdinMVC.PL.ViewModels.AccountVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace AhMedAladdinMVC.PL.Controllers
{
	[AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IEmailSender _emailSender;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
		}
		public IActionResult SignUp()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel signUpVM)
		{
			if (ModelState.IsValid)
			{
				var existingUser = await _userManager.FindByNameAsync(signUpVM.userName);
				if (existingUser is null)
				{
					var signupM = new ApplicationUser()
					{
						FName = signUpVM.firstName,
						LName = signUpVM.lastName,
						Email = signUpVM.email,
						UserName = signUpVM.userName,
						IsAgree = signUpVM.IsAgree
					};
					var result = await _userManager.CreateAsync(signupM, signUpVM.password);
					if (result.Succeeded)
					{
						return RedirectToAction(nameof(SignIn));
					}
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Already Have Same User!");
				}
			}
			return View(signUpVM);
		}
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel signInVM)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(signInVM.EmailOrUsername);
				if (user is null)
					user = await _userManager.FindByNameAsync(signInVM.EmailOrUsername);
				if (user is null)
				{
					ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
					return View(signInVM);
				}
				var result = await _signInManager.PasswordSignInAsync(user, signInVM.password, signInVM.RememberMe, true);

				if (result.Succeeded)
				{
					//return RedirectToAction("Index", "Home");
					return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
				}
				if (result.IsLockedOut)
				{
					ModelState.AddModelError(string.Empty, "Try Again After 1 Minute");
				}
				//if (result.IsNotAllowed)
				//{
				//	ModelState.AddModelError(string.Empty, "Email Not Confirmed");
				//	return View(signInVM);
				//}

				ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

			}
			return View(signInVM);
		}
		public async new Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("SignIn", "Account");
		}
		public IActionResult ResetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordVM)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(resetPasswordVM.email);
				if (user is not null)
				{
					var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
					var resetPasswordUrl = Url.Action(nameof(ChangePassword), nameof(AccountController).Replace("Controller", ""), new { email = user.Email, token = resetPasswordToken }, protocol: Request.Scheme, host: Request.Host.ToString());
					await _emailSender.SendEmailAsync(resetPasswordVM.email, "Reset Your Password", resetPasswordUrl);
					TempData["Message"] = "Email Sent Successfully Check Your Inbox!";
				}
				else
				{

					ModelState.AddModelError(string.Empty, "Invalid Email");
				}

			}
			return View(resetPasswordVM);
		}
		public IActionResult ChangePassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordVM)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;
				if (token is not null && email is not null)
				{
					var user = await _userManager.FindByEmailAsync(email);
					if (user is not null)
					{
						var result = await _userManager.ResetPasswordAsync(user, token, changePasswordVM.Newpassword);
						if (!result.Succeeded)
						{
							foreach (var error in result.Errors)
							{
								ModelState.AddModelError(string.Empty, error.Description);
							}
							return View(changePasswordVM);
						}
						TempData["Message"] = "Password has been changed successfully!";
						return RedirectToAction(nameof(SignIn));
					}
					ModelState.AddModelError(string.Empty, "Invaild URL");
				}
			}
			return View(changePasswordVM);
		}
	}
}


