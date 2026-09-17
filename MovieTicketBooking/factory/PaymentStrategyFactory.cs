using MovieTicketBooking.enums;
using strategy.payment;

namespace factory
{
    public static class PaymentStrategyFactory
    {
        public static IPaymentStrategy Get(PaymentType mode) => mode switch
        {
            PaymentType.UPI => new UpiPayment(),
            PaymentType.CARD => new CardPayment(),
            _ => new UpiPayment()
        };
    }
}