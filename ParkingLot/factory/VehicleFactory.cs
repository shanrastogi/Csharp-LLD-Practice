using System;
using enums;
using model;

namespace factory
{
    public static class VehicleFactory
    {
        public static Vehicle Create(string number, VehicleType type) => type switch
        {
            VehicleType.Car => new Car(number),
            VehicleType.Bike => new Bike(number),
            VehicleType.Truck => new Truck(number),
            _ => throw new ArgumentException("Invalid vehicle type")
        };
    }
}