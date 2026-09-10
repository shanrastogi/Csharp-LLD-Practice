using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.model;

namespace CarRental.strategy.payment
{
    public class CreditCardPaymentStategy : IPaymentStrategy
    {
        public bool ProcessPayment(Booking booking)
        {
            Console.WriteLine("Processing credit card payment for booking: " + booking.BookingId);
            return true;
        }
    }
}