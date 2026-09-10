using System.Globalization;
using CarRental.enums;

namespace CarRental.model
{
    public class Booking
    {
        public required string BookingId { get; set; }
        public required User User { get; set; }
        public required Vehicle Vehicle { get; set; }
        public required Branch PickupBranch { get; set; }
        public required Branch DropBranch { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.CREATED;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.PENDING;
        public double Amount { get; set; }

        public override string ToString()
        {
            string format = "d MMM h:mm tt yyyy";
            IFormatProvider culture = CultureInfo.InvariantCulture;

            return "\n" +
                   $"Booking ID: {BookingId}\n" +
                   $"User: {User?.Name ?? "N/A"}\n" +
                   $"Pickup Time: {StartTime.ToString(format, culture)}\n" +
                   $"Drop Time: {EndTime.ToString(format, culture)}\n" +
                   $"Pickup Location: {PickupBranch?.City ?? "N/A"}\n" +
                   $"Drop Location: {DropBranch?.City ?? "N/A"}\n" +
                   $"Vehicle Type: {Vehicle.Type}\n" +
                   $"Vehicle Number Plate: {Vehicle.LicensePlate}\n" +
                   $"Total Hours: {(long)Math.Ceiling((EndTime - StartTime).TotalHours)}\n" +
                   $"PricePerKm: ${Vehicle.PricePerKm}\n" +
                   $"PricePerHr: ${Vehicle.PricePerHour}\n" +
                   $"Amount: ${Amount}\n" +
                   $"Status: {Status}\n";
        }
    }
}