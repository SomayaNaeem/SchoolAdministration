using SchoolAdministration.Services.Identity.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace SchoolAdministration.Services.Identity.Application.Common.Extensions
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Succeess()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
