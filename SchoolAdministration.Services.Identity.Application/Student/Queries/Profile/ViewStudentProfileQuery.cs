using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SchoolAdministration.Services.Identity.Application.Common.Interfaces;
using SchoolAdministration.Services.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolAdministration.Services.Identity.Application.Student.Queries.Profile
{
	public class ViewStudentProfileQuery : IRequest<ViewStudentProfileDto>
	{
	}
	public class ViewProfileQueryHandler : IRequestHandler<ViewStudentProfileQuery, ViewStudentProfileDto>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;
		public ViewProfileQueryHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService, IMapper mapper)
		{
			_userManager = userManager;
			_currentUserService = currentUserService;
			_mapper = mapper;
		}
		public async Task<ViewStudentProfileDto> Handle(ViewStudentProfileQuery request, CancellationToken cancellationToken)
		{
			return _mapper.Map<ViewStudentProfileDto>(await _userManager.FindByIdAsync(_currentUserService.UserId));
		}
	}
}
