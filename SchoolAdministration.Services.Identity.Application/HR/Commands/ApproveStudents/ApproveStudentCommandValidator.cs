using FluentValidation;
using SchoolAdministration.Services.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.HR.Commands.ApproveStudents
{
	public class ApproveStudentCommandValidator:AbstractValidator<ApproveStudentCommand>
	{
		public ApproveStudentCommandValidator()
		{
			RuleFor(s => s.Id).NotEmpty();
			RuleFor(s => s.Status).NotEmpty().IsInEnum<ApproveStudentCommand,AcceptanceStatus>();
		}
	}
}
