﻿using System;

namespace NCore.Nancy.Api.Contracts
{
    public class EntityDto : IHasIdDto
    {
        public DateTime? CreationDate { get; set; }
        public int Version { get; set; }
        public long? Id { get; set; }
    }
}