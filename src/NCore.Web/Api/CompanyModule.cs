using System;
using System.Collections.Generic;
using Nancy;
using Nancy.Responses;
using NHibernate;

namespace NCore.Web.Api
{
    public abstract class CompanyModule<T, TDto> : CRUDModule<T, TDto>
        where T : IEntity, IHasCompany
    {
        private readonly ICompanyContext _companyContext;

        protected CompanyModule(ICompanyContext companyContext)
            :base("Company/{companyId}")
        {
            _companyContext = companyContext;
            Before += SetContext;
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
                }, new DefaultJsonSerializer()) {StatusCode = HttpStatusCode.BadRequest};
        }
    }
}