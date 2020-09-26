using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.HR.Commands.Signup
{
	public class HrSignupCommandValidator:AbstractValidator<HrSignupCommand>
	{
		public HrSignupCommandValidator()
		{
			RuleFor(s => s.Name).NotEmpty();
			RuleFor(s => s.Password).NotEmpty().MinimumLength(6);
			RuleFor(s => s.Email).NotEmpty().EmailAddress();
			RuleFor(s => s.PhoneNumber).NotEmpty().Matches(@"^\+20\d{10}$").WithMessage("Please specify a valid Egypt mobile phone number of 12 digits only starts with +20"); ;
		}
	}
}
