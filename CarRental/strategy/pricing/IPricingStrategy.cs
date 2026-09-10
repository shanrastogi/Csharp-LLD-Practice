using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.model;

namespace CarRental.strategy.pricing
{
    public interface IPricingStrategy
    {
        double CalculatePrice(Vehicle vehicle, DateTime start, DateTime end, double distanceKm);
    }
}