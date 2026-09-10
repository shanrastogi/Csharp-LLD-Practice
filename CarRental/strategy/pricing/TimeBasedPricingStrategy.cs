using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.model;

namespace CarRental.strategy.pricing
{
    public class TimeBasedPricingStrategy : IPricingStrategy
    {
        public double CalculatePrice(Vehicle vehicle, DateTime start, DateTime end, double distanceKm)
        {
            long hours = (long)Math.Ceiling((end - start).TotalHours);
            return hours * vehicle.PricePerHour;
        }
    }
}