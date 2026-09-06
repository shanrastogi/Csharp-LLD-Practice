using System;

namespace strategy.payment
{
    public class CardPayment : IPaymentStrategy
    {
        public bool Pay(double amount) { Console.WriteLine($"Paid {amount} via Card"); return true; }
    }
}