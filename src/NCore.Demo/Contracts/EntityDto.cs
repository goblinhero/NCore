using System;

namespace NCore.Demo.Contracts
{
    public class EntityDto
    {
        public long? Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public int Version { get; set; }
    }
}