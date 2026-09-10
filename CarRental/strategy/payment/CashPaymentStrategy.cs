using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.model;

namespace CarRental.strategy.payment
{
    public class CashPaymentStrategy : IPaymentStrategy
    {
        public bool ProcessPayment(Booking booking)
        {
            Console.WriteLine("Processing cash payment for booking: " + booking.BookingId);
            return true;
        }
    }
}