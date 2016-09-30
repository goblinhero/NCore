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
        private readonly CustomerDto _dto;

        public CustomerCreator(CustomerDto dto)
        {
            _dto = dto;
        }

        public override bool TryCreate(ISession session, out Customer entity, out IEnumerable<string> errors)
        {
            entity = new Customer
            {
                CompanyName = _dto.CompanyName,
                Address = _dto.Address.ConvertToValueType(),
                CreationDate = DateTime.UtcNow
            };
            return entity.IsValid(out errors);
        }
    }
}