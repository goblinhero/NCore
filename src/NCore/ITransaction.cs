using System;

namespace NCore
{
    public interface ITransaction : IHasId, IIsValidatable
    {
        DateTime? CreationDate { get; }
    }
}