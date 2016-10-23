using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Responses;
using NHibernate;

namespace NCore.Web.Api
{
    public abstract class HasCompanyModule<T, TDto> : CRUDModule<T, TDto>
        where T : IEntity, IHasCompany
    {
        protected readonly ICompanyContext _companyContext;

        protected HasCompanyModule(ICompanyContext companyContext)
            :base("Company/{companyId}/")
        {
            _companyContext = companyContext;
            Before += SetContext;
        }

        private Response SetContext(NancyContext ctx)
        {
            long companyId = ctx.Parameters.companyId;
            IEnumerable<string> errors;
            Action<ISession> enableFilter = s =>
            {
                s.EnableFilter(SessionHelper.CompanyFilter).SetParameter("companyId", companyId);
            };
            _sessionHelper.AddSessionCreateStep(enableFilter);
            return _companyContext.TrySetCompanyId(companyId, out errors)
                ? null
                : new JsonResponse(new
                {
                    Success = false,
                    Errors = errors
                }, new DefaultJsonSerializer()) {StatusCode = HttpStatusCode.BadRequest};
        }
    }
}