using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.enums;
using CarRental.model;
using Microsoft.VisualBasic;

namespace CarRental.strategy.booking
{
    public class CheapestBookingStrategy : IBookingStrategy
    {
        private readonly PricingStrategyType _pricingType;

        public CheapestBookingStrategy(PricingStrategyType pricingStrategyType)
        {
            _pricingType = pricingStrategyType;

        }
        public Vehicle? BookVehicle(List<Vehicle> vehicles)
        {
            var sortedVehicles = vehicles.OrderBy(v => _pricingType == PricingStrategyType.TIME_BASED ? v.PricePerHour : v.PricePerKm).ToList();
            foreach (var vehicle in sortedVehicles)
            {
                if (vehicle.CompareAndSetIsBooked(expect: false, update: true))
                {
                    return vehicle;
                }
            }
            return null;
        }

    }

}