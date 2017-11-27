﻿using System;
using System.Collections.Generic;
using NSI.DC.PricingPackageRepository;

namespace NSI.Repository.Interfaces
{
    public interface IPricingPackageRepository
    {
        PricingPackageDto GetPricingPackage(int pricingPackageId);
        IEnumerable<PricingPackageDto> GetAllPricingPackages();
        PricingPackageDto SavePricingPackage(PricingPackageDto pricingPackage);
        bool DeletePricingPackage(int id);
    }
}
