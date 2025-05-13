using AhMedAladdinMVC.DAL.Models;
using AhMedAladdinMVC.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AhMedAladdinMVC.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
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
					var result=	await _userManager.CreateAsync(signupM, signUpVM.password);
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
	}
}
