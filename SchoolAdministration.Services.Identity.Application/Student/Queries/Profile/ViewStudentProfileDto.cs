using SchoolAdministration.Services.Identity.Application.Common.Mappings;
using SchoolAdministration.Services.Identity.Domain.Entities;
using SchoolAdministration.Services.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.Student.Queries.Profile
{
	public class ViewStudentProfileDto:IMapFrom<ApplicationUser>
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime DateOfBirth { get; set; }
		public AcceptanceStatus AcceptanceStatus { get; set; }
	}
}
