using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Responses;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Queries;
using NCore.Web;
using NHibernate;

namespace NCore.Demo.Modules
{
    public class InvoiceModule : NancyModule
    {
        private readonly ICompanyContext _companyContext;
        private readonly SessionHelper _sessionHelper = new SessionHelper();

        public InvoiceModule(ICompanyContext companyContext)
        {
            _companyContext = companyContext;
            Before += SetContext;
            var staticRoutes = new BaseRoutes(typeof(Invoice).Name, "Company/{companyId}/");
            Get[staticRoutes.Base] = p => GetList(p.customerId);
        }
        private Response SetContext(NancyContext ctx)
        {
            IEnumerable<string> errors;
            Action<ISession> enableFilter = s =>
            {
                s.EnableFilter(SessionHelper.CompanyFilter).SetParameter("companyId", ctx.Parameters.companyId);
            };
            _sessionHelper.AddSessionCreateStep(enableFilter);
            return _companyContext.TrySetCompanyId(ctx.Parameters.companyId, out errors)
                ? null
                : new JsonResponse(new
                {
                    Success = false,
                    Errors = errors
                }, new DefaultJsonSerializer())
                { StatusCode = HttpStatusCode.BadRequest };
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