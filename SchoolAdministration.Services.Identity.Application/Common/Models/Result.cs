using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Success = succeeded;
            Errors = errors.ToArray();
        }

        public bool Success { get; set; }
        public string[] Errors { get; set; }
        public static Result Succeess()
        {
            return new Result(true, new string[] { });
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }
    }
}
