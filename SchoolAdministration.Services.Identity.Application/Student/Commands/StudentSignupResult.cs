using SchoolAdministration.Services.Identity.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.Student.Commands
{
	public class StudentSignupResult:Result
	{
		public StudentSignupResult(bool success,IEnumerable<string>errors,string id):base(success,errors)
		{
			Id = id;
		}
		public string  Id { get; set; }
	}
}
