using System;

namespace strategy.payment
{
    public class UpiPayment : IPaymentStrategy
    {
        public bool Pay(double amount) { Console.WriteLine($"Paid {amount} via UPI"); return true; }
    }
}