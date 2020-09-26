using SchoolAdministration.Services.Identity.Application.Common.Mappings;
using SchoolAdministration.Services.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.HR.Queries.Profile
{
	public class ViewHRProfileDto:IMapFrom<ApplicationUser>
	{
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
	}
}
