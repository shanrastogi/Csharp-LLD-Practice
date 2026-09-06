using enums;

namespace model
{
    public class Bike : Vehicle
    {
        public Bike(string number) : base(number, VehicleType.Bike) { }
    }
}