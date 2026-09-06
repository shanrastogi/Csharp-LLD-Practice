using System;
using enums;

namespace strategy.pricing
{
    public class FlatRatePricing : IPricingStrategy
    {
        public double CalculateFee(VehicleType type, DateTime entryTime, DateTime exitTime) => 50.0;
    }
}