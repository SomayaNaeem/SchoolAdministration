using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAdministration.Services.Identity.Application.Student.Commands;
using SchoolAdministration.Services.Identity.Application.Student.Queries.Profile;

namespace SchoolAdministration.Services.Identity.WebUI.Controllers
{
    public class StudentController : ApiController
    {
        [HttpPost("SignUp")]
        public async Task<StudentSignupResult> UserSignUp(StudentSignupCommand command)
        {
            return await Mediator.Send(command);
        }
        [Authorize(Roles ="Student", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("Profile")]
        public async Task<ViewStudentProfileDto> ViewProfile()
        {
            return await Mediator.Send(new ViewStudentProfileQuery());
        }
    }
}