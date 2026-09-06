using System;
using enums;

namespace model
{
    public class Ticket
    {
        public required string TicketId { get; set; }
        public required DateTime EntryTime { get; set; }
        public required Vehicle Vehicle { get; set; }
        public required string FloorId { get; set; }
        public required string SpotId { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    }
}