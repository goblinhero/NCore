﻿using System;
using System.Collections.Generic;

namespace NCore.Nancy.Exceptions
{
    public class ValidationException : Exception
    {
        public IIsValidatable ObjectValidated { get; }
        public IEnumerable<string> Errors { get; }

        public ValidationException(IIsValidatable objectValidated, IEnumerable<string> errors)
        {
            ObjectValidated = objectValidated;
            Errors = errors;
        }
    }
}