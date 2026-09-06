using enums;

namespace model
{
    public abstract class Vehicle
    {
        public string Number { get; }
        public VehicleType Type { get; }

        protected Vehicle(string number, VehicleType type)
        {
            Number = number;
            Type = type;
        }
    }
}