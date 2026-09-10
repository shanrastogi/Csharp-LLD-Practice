using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.model;
using CarRental.strategy.payment;

namespace CarRental.service
{
    public class PaymentProcessor
    {
        private readonly IPaymentStrategy paymentStrategy;

        public PaymentProcessor(IPaymentStrategy _strategy)
        {
            paymentStrategy = _strategy;
        }

        public bool Pay(Booking booking)
        {
            bool success = paymentStrategy.ProcessPayment(booking);
            if (success)
            {
                booking.PaymentStatus = enums.PaymentStatus.SUCCESS;
            }
            else
            {
                booking.PaymentStatus = enums.PaymentStatus.FAILED;
                Console.WriteLine("Payment Failed");
            }
            return success;
        }
    }
}