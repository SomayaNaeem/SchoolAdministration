using System;
using Microsoft.AspNetCore.Identity;
using SchoolAdministration.Services.Identity.Domain.Enums;

namespace SchoolAdministration.Services.Identity.Domain.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
		public AcceptanceStatus AcceptanceStatus { get; set; }
		public DateTime DateOfBirth { get; set; }
		public UserType UserType { get; set; }
	}
}
