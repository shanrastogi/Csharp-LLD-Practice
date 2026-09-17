namespace MovieTicketBooking.enums
{
    public enum BookingStatus
    {
        CREATED, CONFIRMED, FAILED, CANCELLED
    }

    public enum PaymentType
    {
        CARD, UPI
    }

    public enum SeatStatus
    {
        AVAILABLE, LOCKED, BOOKED
    }

    public enum SeatType
    {
        REGULAR, RECLINER
    }
}