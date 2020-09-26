using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }
}
