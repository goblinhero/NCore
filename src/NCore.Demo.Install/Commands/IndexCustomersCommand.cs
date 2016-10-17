using NCore.Demo.Contracts;
using NCore.Demo.SearchIndexes;

namespace NCore.Demo.Install.Commands
{
    public class IndexCustomersCommand : BaseIndexCommand<CustomerDto, CustomerSearchIndex>
    {
        public IndexCustomersCommand(string index)
            : base(index)
        {
        }

        public override CustomerSearchIndex[] Convert(CustomerDto dto)
        {
            return new[]
            {
                CustomerSearchIndex.FromCustomer(dto)
            };
        }
    }
}