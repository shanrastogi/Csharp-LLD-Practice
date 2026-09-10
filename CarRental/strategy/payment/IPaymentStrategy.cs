using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.model;

namespace CarRental.strategy.payment
{
    public interface IPaymentStrategy
    {
        public bool ProcessPayment(Booking booking);
    }
}