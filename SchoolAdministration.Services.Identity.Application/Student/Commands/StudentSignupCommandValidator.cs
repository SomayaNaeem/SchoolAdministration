using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.Student.Commands
{
	public class StudentSignupCommandValidator:AbstractValidator<StudentSignupCommand>
	{
		public StudentSignupCommandValidator()
		{
			RuleFor(s => s.Name).NotEmpty();
			RuleFor(s => s.Password).NotEmpty().MinimumLength(6);
			RuleFor(s => s.Email).NotEmpty().EmailAddress();
			RuleFor(s => s.DateOfBirth).NotEmpty().Must(AgeValidate).WithMessage("Invalid date student age must be 18 or greater than 18");
			RuleFor(s => s.PhoneNumber).NotEmpty().Matches(@"^\+20\d{10}$").WithMessage("Please specify a valid Egypt mobile phone number of 12 digits only starts with +20"); ;
		}

		private bool AgeValidate(DateTime value)
		{
			DateTime now = DateTime.Today;
			int age = now.Year - Convert.ToDateTime(value).Year;
			if (age < 18)
				return false;
			return true;

		}
	}
}
