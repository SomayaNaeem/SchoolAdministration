using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.HR.Commands.UpdateStudentProfile
{
	public class UpdateStudentProfileCommandValidator:AbstractValidator<UpdateStudentProfileCommand>
	{
		public UpdateStudentProfileCommandValidator()
		{
			RuleFor(s => s.Email).NotEmpty().EmailAddress();
			RuleFor(s => s.Name).NotEmpty();
			RuleFor(s => s.PhoneNumber).NotEmpty().Matches(@"^\+20\d{10}$").WithMessage("Please specify a valid Egypt mobile phone number of 12 digits only starts with +20");
		}
	}
}
