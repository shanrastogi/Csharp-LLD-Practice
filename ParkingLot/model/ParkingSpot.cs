using enums;

namespace model
{
    public class ParkingSpot
    {
        public string Id { get; }
        public VehicleType AllowedType { get; }

        private bool _occupied = false;
        private readonly object _lock = new object();

        public ParkingSpot(string id, VehicleType allowedType)
        {
            Id = id;
            AllowedType = allowedType;
        }

        public bool TryOccupy()
        {
            lock (_lock)
            {
                if (!_occupied)
                {
                    _occupied = true;
                    return true;
                }
                return false;
            }
        }

        public void Vacate() { lock (_lock) { _occupied = false; } }
        public bool IsOccupied() { lock (_lock) { return _occupied; } }
    }
}