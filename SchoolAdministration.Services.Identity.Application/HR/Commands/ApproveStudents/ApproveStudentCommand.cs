using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolAdministration.Services.Identity.Application.Common.Exceptions;
using SchoolAdministration.Services.Identity.Application.Common.Interfaces;
using SchoolAdministration.Services.Identity.Domain.Entities;
using SchoolAdministration.Services.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolAdministration.Services.Identity.Application.HR.Commands.ApproveStudents
{
	public class ApproveStudentCommand:IRequest
	{
		public string Id { get; set; }
		public AcceptanceStatus Status { get; set; }
	}
	public class ApproveStudentCommandHandler : IRequestHandler<ApproveStudentCommand>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ICurrentUserService _currentUserService;
		public ApproveStudentCommandHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
		{
			_userManager = userManager;
			_currentUserService = currentUserService;
		}
		public async Task<Unit> Handle(ApproveStudentCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.Id);
			if (user == null)
			{
				throw new NotFoundException(nameof(ApplicationUser), request.Id);
			}
			user.AcceptanceStatus = request.Status;
			await _userManager.UpdateAsync(user);
			return Unit.Value;
		}
	}
}
