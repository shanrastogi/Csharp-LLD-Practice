using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.enums;

namespace CarRental.model
{
    public abstract class Vehicle
    {
        public string LicensePlate { get; set; }
        public VehicleStatus Status { get; set; }
        public double PricePerHour { get; set; }
        public double PricePerKm { get; set; }
        public VehicleType Type { get; set; }
        public int BookingCount { get; set; } = 0;

        // C# equivalent of AtomicBoolean using an int (0 for false, 1 for true)
        private int _isBooked = 0;

        public bool IsBooked
        {
            get => Volatile.Read(ref _isBooked) == 1;
            set => Interlocked.Exchange(ref _isBooked, value ? 1 : 0);
        }

        public Vehicle(string licensePlate, double pricePerHour, double pricePerKm, VehicleType type)
        {
            LicensePlate = licensePlate;
            Status = VehicleStatus.AVAILABLE;
            PricePerHour = pricePerHour;
            PricePerKm = pricePerKm;
            Type = type;
        }

        public void IncrementBookingCount()
        {
            BookingCount++;
        }

        public bool CompareAndSetIsBooked(bool expect, bool update)
        {
            int expectedInt = expect ? 1 : 0;
            int updateInt = update ? 1 : 0;
            return Interlocked.CompareExchange(ref _isBooked, updateInt, expectedInt) == expectedInt;
        }
    }
}