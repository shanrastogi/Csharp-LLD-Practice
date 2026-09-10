using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.model;

namespace CarRental.strategy.pricing
{
    public class DistanceBasedPricingStrategy : IPricingStrategy
    {
        public double CalculatePrice(Vehicle vehicle, DateTime start, DateTime end, double distanceKm)
        {
            Console.WriteLine("Distance in KM: " + distanceKm);
            return distanceKm * vehicle.PricePerKm;
        }
    }
}