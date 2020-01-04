﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Entities
{
    public abstract class PriceSystemGroup:CodedNomenclatorBase,IPriceSystemGroup
    {
        public string PriceSystem { get; set; }
    }
}
