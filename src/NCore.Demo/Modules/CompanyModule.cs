using System.Collections.Generic;
using NCore.Demo.Commands;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Queries;
using NCore.Web.Api;
using NCore.Web.Commands;

namespace NCore.Demo.Modules
{
    public class CompanyModule : CRUDModule<Company, CompanyDto>
    {
        public CompanyModule()
        {
            Get[_staticRoutes.Base] = p => GetList();
        }

        private object GetList()
        {
            IEnumerable<string> errors;
            IList<CompanyDto> result;
            return _sessionHelper.TryQuery(new FindCompanyQuery(), out result, out errors)
                ? new
                {
                    Success = true,
                    Result = result
                }
                : (object) new
                {
                    Success = false,
                    Errors = errors
                };
        }

        protected override ICreator GetCreator(IDictionary<string, object> dto)
        {
            return new CompanyCreator(dto);
        }

        protected override ICommand GetUpdater(long id, IDictionary<string, object> dto)
        {
            return new CompanyUpdater(id, dto);
        }

        protected override ICommand GetDeleter(long id)
        {
            return new CompanyDeleter(id);
        }
    }
}