using System.Collections.Generic;
using NCore.Extensions;
using NCore.Web;

namespace NCore.Demo.Utilities
{
    public class CompanyContext : ICompanyContext
    {
        private long? _currentCompany;
        public long? CurrentCompany => _currentCompany;
        public bool TrySetCompanyId(long companyId, out IEnumerable<string> errors)
        {
            //A good place to check if the current user is allowed to whatever it is
            //she is doing
            _currentCompany = companyId;
            return this.Success(out errors);
        }
    }
}