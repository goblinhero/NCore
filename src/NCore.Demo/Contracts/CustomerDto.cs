using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCore.Demo.Contracts
{
    public class CustomerDto:EntityDto
    {
        public string CompanyName { get; set; }
        public AddressDto Address { get; set; }
    }
}
