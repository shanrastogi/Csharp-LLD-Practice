using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.model;

namespace CarRental.strategy.booking
{
    public class LeastBookedStartegy : IBookingStrategy
    {
        public Vehicle? BookVehicle(List<Vehicle> vehicles)
        {
            var sortedVehicles = vehicles.OrderBy(v => v.BookingCount).ToList();

            foreach (var vehicle in vehicles)
            {
                if (vehicle.CompareAndSetIsBooked(false, true))
                {
                    return vehicle;
                }
            }
            return null;
        }
    }
}