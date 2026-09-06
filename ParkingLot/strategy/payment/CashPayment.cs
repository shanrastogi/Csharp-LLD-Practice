using System;

namespace strategy.payment
{
    public class CashPayment : IPaymentStrategy
    {
        public bool Pay(double amount) { Console.WriteLine($"Paid {amount} via Cash"); return true; }
    }
}