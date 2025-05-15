using AhMedAladdinMVC.BLL.IRepositories;
using AhMedAladdinMVC.BLL.Repositories;
using AhMedAladdinMVC.DAL.Data;
using AhMedAladdinMVC.DAL.Models;
using AhMedAladdinMVC.PL.Helpers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AhMedAladdinMVC.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews(options=>
            {
				// كل الكنترولز هتبقي اكن محطوط عليها [Authorize]
				//////////////
				var policy = new AuthorizationPolicyBuilder()
				   .RequireAuthenticatedUser()
				   .Build();
				options.Filters.Add(new AuthorizeFilter(policy));
			   //////////////
            
            }).AddRazorRuntimeCompilation();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))).AddMyAppExtensions();


            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(
                options =>
                {
                    options.Password.RequiredLength = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                    options.Lockout.MaxFailedAccessAttempts = 3;
				}
                ).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); // 👈 دي مهمة جدًا
			;
			///////////////////////
			///هنا بعدل ال config بتاع ال identity
			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Account/SignIn";
				options.LogoutPath = "/Account/SignOut";
				//options.AccessDeniedPath = "/Account/AccessDenied";
			});
			///////////////////////
			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
			app.UseAuthentication(); // 👈 لازم الأول
			app.UseAuthorization();  // 👈 بعده			
			app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
