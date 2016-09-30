using System;

namespace NCore
{
    public interface IEntity : IHasId, IIsValidatable
    {
        DateTime? CreationDate { get; set; }
        int Version { get; }
    }
}