using SchoolAdministration.Services.Identity.Application.Common.Extensions;
using SchoolAdministration.Services.Identity.Application.Common.Interfaces;
using SchoolAdministration.Services.Identity.Application.Common.Models;
using SchoolAdministration.Services.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SchoolAdministration.Services.Identity.Application.Student.Commands;
using SchoolAdministration.Services.Identity.Domain.Enums;
using SchoolAdministration.Services.Identity.Application.HR.Commands.Signup;

namespace SchoolAdministration.Services.Identity.Infrastructure.Identity
{
	public class IdentityService : IIdentityService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		public IdentityService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<(Result Result, string id)> CreateHrAsync(HrSignupCommand HrSignUp)
		{
			var user = new ApplicationUser
			{
				Name = HrSignUp.Name,
				PhoneNumber = HrSignUp.PhoneNumber,
				Email = HrSignUp.Email,
				UserName = HrSignUp.Email,
				UserType = UserType.HR,
			};
			var result = await _userManager.CreateAsync(user, HrSignUp.Password);
			string userId = null;
			if (result.Succeeded)
			{
				userId = user.Id;
				await _userManager.AddToRoleAsync(user, UserType.HR.ToString());
			}
			return (result.ToApplicationResult(), userId);
		}

		public async Task<(Result Result, string id)> CreateStudentAsync(StudentSignupCommand studentSignUp)
		{
			var user = new ApplicationUser
			{
				Name = studentSignUp.Name,
				PhoneNumber = studentSignUp.PhoneNumber,
				Email = studentSignUp.Email,
				UserName=studentSignUp.Email,
				UserType = UserType.Student,
				AcceptanceStatus=Domain.Enums.AcceptanceStatus.Pending,
				DateOfBirth=studentSignUp.DateOfBirth

			};
			var result = await _userManager.CreateAsync(user, studentSignUp.Password);
			string userId = null;
			if (result.Succeeded)
			{
				userId = user.Id;
				await _userManager.AddToRoleAsync(user, UserType.Student.ToString());
			}
			return (result.ToApplicationResult(), userId);
		}

		public async Task<ApplicationUser> GetUser(string email)
		{
			return await _userManager.FindByEmailAsync(email);
		}

		public async Task<string> GetUserNameAsync(string userId)
		{
			var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
			return user.UserName;
		}
	}
}
