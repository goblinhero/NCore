using System.Collections.Generic;
using NHibernate;

namespace NCore.Nancy.Commands
{
    public interface ICommand
    {
        bool TryExecute(ISession session, out IEnumerable<string> errors);
    }
}