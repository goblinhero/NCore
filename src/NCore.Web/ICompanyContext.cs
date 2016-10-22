using System.Collections.Generic;

namespace NCore.Web
{
    public interface ICompanyContext
    {
        long? CurrentCompany { get; }
        bool TrySetCompanyId(long companyId, out IEnumerable<string> errors);
    }
}