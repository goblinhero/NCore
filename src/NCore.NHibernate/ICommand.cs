using System.Collections.Generic;
using NHibernate;

namespace NCore.NHibernate
{
    public interface ICommand
    {
        bool TryExecute(ISession session, out IEnumerable<string> errors);
    }
}