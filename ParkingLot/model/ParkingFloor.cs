using System.Collections.Concurrent;
using enums;

namespace model
{
    public class ParkingFloor
    {
        public string Id { get; }
        public ConcurrentDictionary<string, ParkingSpot> Spots { get; } = new();

        public ParkingFloor(string id) { Id = id; }

        public void AddSpot(ParkingSpot spot) { Spots.TryAdd(spot.Id, spot); }

        public ParkingSpot? FindAvailableSpot(VehicleType type)
        {
            foreach (var spot in Spots.Values)
            {
                if (spot.AllowedType == type && !spot.IsOccupied() && spot.TryOccupy())
                    return spot;
            }
            return null;
        }
    }
}