using MediatR;
using SchoolAdministration.Services.Identity.Application.Common.Interfaces;
using SchoolAdministration.Services.Identity.Application.Common.Models;
using SchoolAdministration.Services.Identity.Domain.Entities;
using SchoolAdministration.Services.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolAdministration.Services.Identity.Application.Student.Commands
{
	public class StudentSignupCommand:IRequest<StudentSignupResult>
	{
		public string Name { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
	public class SignupCommandHandler : IRequestHandler<StudentSignupCommand, StudentSignupResult>
	{
		private readonly IIdentityService _identityService;
		public SignupCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}
		public async Task<StudentSignupResult> Handle(StudentSignupCommand request, CancellationToken cancellationToken)
		{
			var (result, id) = await _identityService.CreateStudentAsync(request);
			return new StudentSignupResult(result.Success, result.Errors, id);
		}
	}
}
