using System;

namespace NCore.Nancy.Contracts
{
    public class EntityDto: IHasIdDto
    {
        public long? Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public int Version { get; set; }
    }
}