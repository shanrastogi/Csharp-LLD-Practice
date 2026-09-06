using System;
using System.Globalization;
using enums;
using factory;
using model;
using service;

class Program
{
    static void Main()
    {
        ParkingLot lot = ParkingLot.Instance;
        lot.PricingStrategy = PricingStrategyFactory.Get(PricingStrategyType.FlatRate);

        EntryGate entryGate = new EntryGate("EG1");
        ExitGate exitGate = new ExitGate("XG1");

        ParkingFloor floor1 = new ParkingFloor("Floor1");
        floor1.AddSpot(new ParkingSpot("F1S1", VehicleType.Bike));
        floor1.AddSpot(new ParkingSpot("F1S2", VehicleType.Car));
        floor1.AddSpot(new ParkingSpot("F1S3", VehicleType.Truck));
        floor1.AddSpot(new ParkingSpot("F1S4", VehicleType.Car));
        lot.AddFloor(floor1);

        Console.WriteLine("--------------------------");

        Vehicle bike1 = VehicleFactory.Create("KA01AB1234", VehicleType.Bike);
        Vehicle car = VehicleFactory.Create("KA01AB1234", VehicleType.Car);

        string entryTimeStr = "21 May 7:30 AM 2025";
        string dateFormat = "dd MMM h:mm tt yyyy";
        DateTime entryTime = DateTime.ParseExact(entryTimeStr, dateFormat, CultureInfo.InvariantCulture);

        Ticket? ticket = entryGate.ParkVehicle(car, entryTime);

        Console.WriteLine("--------------------------");
        lot.PrintStatus();
        Console.WriteLine("--------------------------");

        if (ticket != null)
        {
            string exitTimeStr = "21 May 1:15 PM 2025";
            DateTime exitTime = DateTime.ParseExact(exitTimeStr, dateFormat, CultureInfo.InvariantCulture);
            exitGate.UnparkVehicle(ticket.TicketId, exitTime, PaymentMode.Upi);
        }

        Console.WriteLine("--------------------------");
        lot.PrintStatus();
    }
}