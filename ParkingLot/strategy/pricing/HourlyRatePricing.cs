using System;
using enums;

namespace strategy.pricing
{
    public class HourlyRatePricing : IPricingStrategy
    {
        public double CalculateFee(VehicleType type, DateTime entryTime, DateTime exitTime) =>
            (exitTime - entryTime).TotalHours * 20.0;
    }
}