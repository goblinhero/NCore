using System.Collections.Generic;

namespace NCore
{
    public interface IIsValidatable
    {
        bool IsValid(out IEnumerable<string> errors);
    }
}