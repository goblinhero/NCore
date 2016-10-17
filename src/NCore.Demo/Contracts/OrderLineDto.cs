using NCore.Web.Api.Contracts;

namespace NCore.Demo.Contracts
{
    public class OrderLineDto : EntityDto
    {
        public long OrderId { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public int? Index { get; set; }
    }
}