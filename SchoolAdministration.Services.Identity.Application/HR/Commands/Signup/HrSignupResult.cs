using SchoolAdministration.Services.Identity.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.HR.Commands.Signup
{
	public class HrSignupResult:Result
	{
		public HrSignupResult(bool success, IEnumerable<string> errors, string id) : base(success, errors)
		{
			Id = id;
		}
		public string Id { get; set; }
	}
}
