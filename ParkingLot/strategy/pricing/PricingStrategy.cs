using System;
using enums;

namespace strategy.pricing
{
    public interface IPricingStrategy
    {
        double CalculateFee(VehicleType type, DateTime entryTime, DateTime exitTime);
    }
}