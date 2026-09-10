using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.model;

namespace CarRental.strategy.booking
{
    public interface IBookingStrategy
    {
        Vehicle? BookVehicle(List<Vehicle> vehicles);
    }
}