using SchoolAdministration.Services.Identity.Application.Common.Models;
using SchoolAdministration.Services.Identity.Application.HR.Commands.Signup;
using SchoolAdministration.Services.Identity.Application.Student.Commands;
using System.Threading.Tasks;

namespace SchoolAdministration.Services.Identity.Application.Common.Interfaces
{
	public interface IIdentityService
	{
		Task<string> GetUserNameAsync(string userId);
		Task<(Result Result, string id)> CreateStudentAsync(StudentSignupCommand studentSignUp);
		//Task<ApplicationUser> GetUser(string email);
		Task<(Result Result, string id)> CreateHrAsync(HrSignupCommand HrSignUp);

	}
}
