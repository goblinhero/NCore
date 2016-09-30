using System;

namespace NCore
{
    public interface IEntity : IHasId, IIsValidatable
    {
        DateTime? CreationDate { get; }
        int Version { get; }
    }
}