using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolAdministration.Services.Identity.Application.Common.Interfaces;
using SchoolAdministration.Services.Identity.Application.Common.Models;
using SchoolAdministration.Services.Identity.Domain.Entities;
using SchoolAdministration.Services.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolAdministration.Services.Identity.Application.Student.Queries.StudentsList
{
	public class ViewStudentsListQuery:IRequest<PagedList<ViewStudentsListDto>>
	{
		public AcceptanceStatus AcceptanceStatus { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
	}
	public class ViewStudentsListQueryHandler : IRequestHandler<ViewStudentsListQuery, PagedList<ViewStudentsListDto>>
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ICurrentUserService _currentUserService;
		private readonly IMapper _mapper;
		public ViewStudentsListQueryHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService, IMapper mapper)
		{
			_userManager = userManager;
			_currentUserService = currentUserService;
			_mapper = mapper;
		}
		public async Task<PagedList<ViewStudentsListDto>> Handle(ViewStudentsListQuery request, CancellationToken cancellationToken)
		{
			var students = await _userManager.Users.Where(s=>s.AcceptanceStatus==request.AcceptanceStatus && s.UserType==UserType.Student)
				.ProjectTo<ViewStudentsListDto>(_mapper.ConfigurationProvider)
				.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize)
				.ToListAsync(cancellationToken);
			return new PagedList<ViewStudentsListDto>(students, request.PageSize, request.PageNumber);
		}
	}
}
