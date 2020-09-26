using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAdministration.Services.Identity.Application.Common.Models;
using SchoolAdministration.Services.Identity.Application.HR.Commands.ApproveStudents;
using SchoolAdministration.Services.Identity.Application.HR.Commands.Signup;
using SchoolAdministration.Services.Identity.Application.HR.Commands.UpdateStudentProfile;
using SchoolAdministration.Services.Identity.Application.HR.Queries.Profile;
using SchoolAdministration.Services.Identity.Application.Student.Queries.StudentsList;
using SchoolAdministration.Services.Identity.Domain.Enums;
using static IdentityServer4.IdentityServerConstants;

namespace SchoolAdministration.Services.Identity.WebUI.Controllers
{
    public class HRController : ApiController
    {
        [HttpPost("SignUp")]
        public async Task<HrSignupResult> UserSignUp(HrSignupCommand command)
        {
            return await Mediator.Send(command);
        }
        [Authorize(Roles ="HR", AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet("Profile")]
        public async Task<ViewHRProfileDto> ViewProfile()
        {
            return await Mediator.Send(new ViewHRProfileQuery());
        }
        [Authorize(Roles = "HR", AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet("{acceptanceStatus}")]
        public async Task<ActionResult<PagedList<ViewStudentsListDto>>> Get(AcceptanceStatus acceptanceStatus , int pageSize, int PageNumber)
        {
            return await Mediator.Send(new ViewStudentsListQuery() { AcceptanceStatus = acceptanceStatus, PageNumber = PageNumber, PageSize = pageSize });
        }

        [HttpPut("AcceptStudent")]
        [Authorize(Roles = "HR", AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AcceptStudent(ApproveStudentCommand command)
        {          
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut("UpdateStudent")]
        [Authorize(Roles = "HR", AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateStudent(UpdateStudentProfileCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}