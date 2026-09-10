using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.enums;

namespace CarRental.model
{
    public class Sedan : Vehicle
    {
        public Sedan(string licensePlate, double pricePerHour, double pricePerKm) : base(licensePlate, pricePerHour, pricePerKm, VehicleType.SEDAN) { }
    }
}