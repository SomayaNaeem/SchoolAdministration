using MediatR;
using SchoolAdministration.Services.Identity.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolAdministration.Services.Identity.Application.HR.Commands.Signup
{
	public class HrSignupCommand:IRequest<HrSignupResult>
	{
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
	}
	public class HrSignupCommandHandler : IRequestHandler<HrSignupCommand, HrSignupResult>
	{
		private readonly IIdentityService _identityService;
		public HrSignupCommandHandler(IIdentityService identityService)
		{
			_identityService = identityService;
		}
		public async Task<HrSignupResult> Handle(HrSignupCommand request, CancellationToken cancellationToken)
		{
			var (result, id) = await _identityService.CreateHrAsync(request);
			return new HrSignupResult(result.Success, result.Errors, id);
		}

		
	}
}
