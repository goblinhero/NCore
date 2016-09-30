using System.Collections.Generic;
using System.Configuration;
using NCore.Demo.Domain;
using NCore.Demo.Mappings;
using NCore.Demo.Queries;
using NCore.Nancy;

namespace NCore.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IEnumerable<string> errors;
            SessionHelper.TryInitialize(ConfigurationManager.ConnectionStrings["NCoreConnection"], out errors, null, typeof(CustomerMapping));
            var helper = new SessionHelper();
            IList<Customer> results;
            var result = helper.TryQuery(new FindCustomerQuery(), out results, out errors);
        }
    }
}