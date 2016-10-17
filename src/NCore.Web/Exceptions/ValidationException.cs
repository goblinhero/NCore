using System;
using System.Collections.Generic;

namespace NCore.Web.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(IIsValidatable objectValidated, IEnumerable<string> errors)
        {
            ObjectValidated = objectValidated;
            Errors = errors;
        }

        public IIsValidatable ObjectValidated { get; }
        public IEnumerable<string> Errors { get; }
    }
}