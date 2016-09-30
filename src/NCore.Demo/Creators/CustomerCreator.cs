using System;
using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Extensions;
using NCore.NHibernate;
using NHibernate;

namespace NCore.Demo.Creators
{
    public class CustomerCreator : ICreator<Customer>
    {
        public long? AssignedId { get; set; }

        public bool TryCreate(ISession session, out Customer entity, out IEnumerable<string> errors)
        {
            entity = new Customer {CompanyName = "Hestebiksen ApS", Address = new Address("Diagonalley 14", "Hogwarts", "England"), CreationDate = DateTime.UtcNow};
            return this.Success(out errors);
        }
    }
}