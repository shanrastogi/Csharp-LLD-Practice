using System;
using enums;
using service;

namespace model
{
    public class ExitGate : Gate
    {
        public ExitGate(string id) : base(id) { }

        public void UnparkVehicle(string ticketId, DateTime exitTime, PaymentMode paymentMode)
        {
            ParkingLot.Instance.UnparkVehicle(ticketId, exitTime, paymentMode);
        }
    }
}