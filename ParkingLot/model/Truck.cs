using enums;

namespace model
{
    public class Truck : Vehicle
    {
        public Truck(string number) : base(number, VehicleType.Truck) { }
    }
}