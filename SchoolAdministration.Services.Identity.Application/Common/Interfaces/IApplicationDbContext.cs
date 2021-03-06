﻿using System.Threading;
using System.Threading.Tasks;

namespace SchoolAdministration.Services.Identity.Application.Common.Interfaces
{
	public interface IApplicationDbContext
	{
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
