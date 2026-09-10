namespace CarRental.enums
{
    public enum BookingStatus
    {
        CREATED, CONFIRMED, COMPLETED, FAILED, CANCELLED
    }

    public enum PaymentStatus
    {
        PENDING, SUCCESS, FAILED
    }

    public enum PricingStrategyType
    {
        TIME_BASED, DISTANCE_BASED
    }

    public enum VehicleStatus
    {
        AVAILABLE, BOOKED, IN_SERVICE, DECOMMISSIONED
    }

    public enum VehicleType
    {
        SEDAN, SUV
    }
}