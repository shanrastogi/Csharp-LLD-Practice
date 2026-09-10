using CarRental.enums;
using CarRental.factory;
using CarRental.model;
using CarRental.repository;
using CarRental.service;
using CarRental.strategy.booking;
using CarRental.strategy.payment;
using CarRental.strategy.pricing;

public class Program
{
    public static void Main(string[] args)
    {
        BranchRepository branchRepository = new BranchRepository();
        BookingRepository bookingRepository = new BookingRepository();

        Branch branch1 = new Branch("B1", "New York");
        Branch branch2 = new Branch("B2", "Boston");
        branchRepository.AddBranch(branch1);
        branchRepository.AddBranch(branch2);

        branch1.AddVehicle(VehicleFactory.Get(CarRental.enums.VehicleType.SEDAN, "NY1234", 25, 3.5));
        branch1.AddVehicle(VehicleFactory.Get(CarRental.enums.VehicleType.SEDAN, "NY5678", 20, 3));
        branch1.AddVehicle(VehicleFactory.Get(CarRental.enums.VehicleType.SUV, "NYB100", 30, 4));

        branch2.AddVehicle(VehicleFactory.Get(CarRental.enums.VehicleType.SEDAN, "BO1234", 25, 4));

        User user = new User("U1", "Shan", "qwerty@ex.com");

        BookingService bookingService1 = BookingService.GetInstance(branchRepository, bookingRepository, new LeastBookedStartegy(), new TimeBasedPricingStrategy());

        Console.WriteLine("--------------------------------------");

        bookingService1.BookVehicle("B1", CarRental.enums.VehicleType.SEDAN, DateTime.Now.AddDays(-2), DateTime.Now, user, new CreditCardPaymentStategy(), branch1, branch2, 100.0);

        Console.WriteLine("--------------------------------------");

        BookingService bookingService2 = BookingService.GetInstance(branchRepository, bookingRepository, new CheapestBookingStrategy(PricingStrategyType.DISTANCE_BASED), new DistanceBasedPricingStrategy());

        bookingService2.BookVehicle("B1", CarRental.enums.VehicleType.SEDAN, DateTime.Now.AddDays(-1), DateTime.Now, user, new CashPaymentStrategy(), branch1, branch2, 200.0);

        Console.WriteLine("--------------------------------------");
    }
}
