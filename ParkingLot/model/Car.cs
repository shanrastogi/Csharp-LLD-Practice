using enums;

namespace model
{
    public class Car : Vehicle
    {
        public Car(string number) : base(number, VehicleType.Car) { }
    }
}