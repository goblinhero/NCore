using System;
using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Nancy.Creators;
using NHibernate;

namespace NCore.Demo.Creators
{
    public class CustomerCreator : BaseCreator<Customer>
    {
        private readonly object _dto;

        public CustomerCreator(object dto)
        {
            _dto = dto;
        }

        public override bool TryCreate(ISession session, out Customer entity, out IEnumerable<string> errors)
        {
            entity = new Customer();
            UpdateSimpleProperty(e => e.CompanyName,_dto);
            //TODO: Get Address
            return entity.IsValid(out errors);
        }
    }
}