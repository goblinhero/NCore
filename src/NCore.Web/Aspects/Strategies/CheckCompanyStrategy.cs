using System;
using System.Security;
using NCore.Strategies;
using NHibernate.Event;
using Serilog;

namespace NCore.Web.Aspects.Strategies
{
    public class CheckCompanyStrategy : IStrategy<PostLoadEvent>
    {
        private readonly Func<ICompanyContext> _getCompanyContext;

        public CheckCompanyStrategy(Func<ICompanyContext> getCompanyContext)
        {
            _getCompanyContext = getCompanyContext;
        }

        public bool IsApplicable(PostLoadEvent criteria)
        {
            return criteria.Entity is IHasCompany;
        }

        public void Execute(PostLoadEvent criteria)
        {
            var hasCompany = (IHasCompany) criteria.Entity;
            var context = _getCompanyContext();
            if (context != null && hasCompany.CompanyId.Equals(context.CurrentCompany))
                return;
            Log.Warning($"Tried to load {criteria.Entity.GetType().Name} (ToString: {criteria.Entity} with companyId: {hasCompany.CompanyId}) while working in company: {context?.CurrentCompany}");
            throw new SecurityException("Not allowed to work with this entity in this company.");
        }
    }
}