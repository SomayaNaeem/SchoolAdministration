using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolAdministration.Services.Identity.Application.Common.Exceptions;
using SchoolAdministration.Services.Identity.Application.Common.Interfaces;
using SchoolAdministration.Services.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolAdministration.Services.Identity.Application.HR.Commands.UpdateStudentProfile
{
	public class UpdateStudentProfileCommand:IRequest
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
	}
	public class UpdateStudentProfileCommandHandler : IRequestHandler<UpdateStudentProfileCommand>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ICurrentUserService _currentUserService;
		public UpdateStudentProfileCommandHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
		{
			_userManager = userManager;
			_currentUserService = currentUserService;
		}
		public async Task<Unit> Handle(UpdateStudentProfileCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.Id);
			if (user == null)
			{
				throw new NotFoundException(nameof(ApplicationUser), request.Id);
			}
			user.Name = request.Name;
			user.PhoneNumber = request.PhoneNumber;
			user.Email = request.Email;
			user.UserName = request.Email;
			await _userManager.UpdateAsync(user);
			return Unit.Value;
		}
	}
}
