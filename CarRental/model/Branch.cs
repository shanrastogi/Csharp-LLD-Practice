using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.enums;

namespace CarRental.model
{
    public class Branch
    {
        public string Id { get; set; }
        public string City { get; set; }

        public Dictionary<VehicleType, List<Vehicle>> vehicles = new();

        public Branch(string id, string city)
        {
            Id = id;
            City = city;
        }

        public List<Vehicle> getVehiclesByType(VehicleType type)
        {
            if (vehicles.TryGetValue(type, out var vehicleList))
            {
                return vehicleList;
            }
            return new List<Vehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (!vehicles.TryGetValue(vehicle.Type, out var vehicleList))
            {
                vehicleList = new List<Vehicle>();
                vehicles[vehicle.Type] = vehicleList;
            }
            vehicleList.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            if (vehicles.TryGetValue(vehicle.Type, out var vehicleList))
            {
                vehicleList.Remove(vehicle);
            }
        }
    }
}