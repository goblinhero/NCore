using System.Collections.Generic;
using Nancy;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Queries;
using NCore.Nancy;

namespace NCore.Demo.Modules
{
    public class InvoiceModule : NancyModule
    {
        private readonly SessionHelper _sessionHelper = new SessionHelper();

        public InvoiceModule()
        {
            var staticRoutes = new StaticRoutes(typeof(Invoice).Name);
            Get[staticRoutes.Base] = p => GetList(p.customerId);
        }

        private object GetList(long? customerId)
        {
            IEnumerable<string> errors;
            IList<InvoiceDto> result;
            return _sessionHelper.TryQuery(new FindInvoiceQuery(customerId), out result, out errors)
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
    }
}