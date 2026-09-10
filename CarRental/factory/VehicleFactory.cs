using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CarRental.enums;
using CarRental.model;

namespace CarRental.factory
{
    public static class VehicleFactory
    {
        public static Vehicle Get(VehicleType type, string licensePlate, double pricePerHour, double pricePerKm) => type switch
        {
            VehicleType.SEDAN => new Sedan(licensePlate, pricePerHour, pricePerKm),
            VehicleType.SUV => new SUV(licensePlate, pricePerHour, pricePerKm),
            _ => throw new ArgumentException("Invalid Vehicle Type")
        };
    }
}