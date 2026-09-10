using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CarRental.enums;
using CarRental.model;
using CarRental.repository;
using CarRental.strategy.booking;
using CarRental.strategy.payment;
using CarRental.strategy.pricing;

namespace CarRental.service
{
    public class BookingService
    {
        private static volatile BookingService? _instance;
        private static readonly object _lock = new object();

        private readonly BranchRepository _branchRepository;
        private readonly BookingRepository _bookingRepository;

        public IBookingStrategy BookingStrategy { get; set; }
        public IPricingStrategy PricingStrategy { get; set; }

        private BookingService(BranchRepository branchRepository, BookingRepository bookingRepository, IBookingStrategy bookingStrategy, IPricingStrategy pricingStrategy)
        {
            _branchRepository = branchRepository;
            _bookingRepository = bookingRepository;
            BookingStrategy = bookingStrategy;
            PricingStrategy = pricingStrategy;
        }

        public static BookingService GetInstance(BranchRepository branchRepository, BookingRepository bookingRepository, IBookingStrategy bookingStrategy, IPricingStrategy pricingStrategy)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new BookingService(branchRepository, bookingRepository, bookingStrategy, pricingStrategy);
                    }
                }
            }
            _instance.BookingStrategy = bookingStrategy;
            _instance.PricingStrategy = pricingStrategy;

            return _instance;
        }

        public Booking? BookVehicle(string branchId, VehicleType vehicleType, DateTime start, DateTime end, User user, IPaymentStrategy paymentStrategy, Branch pickupBranch, Branch dropBranch, double distanceInKm)
        {
            Branch branch = _branchRepository.GetBranch(branchId);
            if (branch == null)
            {
                Console.WriteLine("Branch not found");
                return null;
            }

            List<Vehicle> activeVehicles = branch.getVehiclesByType(vehicleType).Where(v => v.Status == VehicleStatus.AVAILABLE && !v.IsBooked).ToList();

            if (activeVehicles.Count == 0)
            {
                Console.WriteLine($"No active {vehicleType} vehicles available.");
                return null;
            }

            Vehicle? vehicle = BookingStrategy.BookVehicle(activeVehicles);
            if (vehicle == null)
            {
                Console.WriteLine("No Vehicle could be booked");
                return null;
            }

            double amount = PricingStrategy.CalculatePrice(vehicle, start, end, distanceInKm);

            Booking booking = new Booking
            {
                BookingId = Guid.NewGuid().ToString(),
                User = user,
                Vehicle = vehicle,
                PickupBranch = pickupBranch,
                DropBranch = dropBranch,
                StartTime = start,
                EndTime = end,
                Amount = amount,
                Status = BookingStatus.CREATED,
                PaymentStatus = PaymentStatus.PENDING
            };

            PaymentProcessor paymentProcessor = new PaymentProcessor(paymentStrategy);
            if (!paymentProcessor.Pay(booking))
            {
                Console.WriteLine("Payment Failed");
                vehicle.IsBooked = false;
                return null;
            }

            booking.Status = BookingStatus.CONFIRMED;
            _bookingRepository.AddBooking(booking);
            vehicle.IncrementBookingCount();
            vehicle.Status = VehicleStatus.BOOKED;

            Console.WriteLine(booking.ToString());
            return booking;
        }

        public void ReturnVehicle(string bookingId)
        {
            Booking? booking = _bookingRepository.GetBookingById(bookingId);
            if (booking == null)
            {
                throw new Exception("Booking not found");
            }
            if (booking.Status != BookingStatus.CONFIRMED)
            {
                throw new Exception("Vehicle is not currently booked");
            }
            booking.Status = BookingStatus.COMPLETED;
            booking.Vehicle.IsBooked = false;
            Branch dropBranch = booking.DropBranch;
            dropBranch.AddVehicle(booking.Vehicle);
            Console.WriteLine($"Vehicle returned to branch {dropBranch.City} : {booking.Vehicle.LicensePlate}");
        }
    }
}