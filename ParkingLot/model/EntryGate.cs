using System;
using service;

namespace model
{
    public class EntryGate : Gate
    {
        public EntryGate(string id) : base(id) { }

        public Ticket? ParkVehicle(Vehicle vehicle, DateTime entryTime)
        {
            return ParkingLot.Instance.ParkVehicle(vehicle, entryTime);
        }
    }
}