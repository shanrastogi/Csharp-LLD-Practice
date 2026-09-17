using MovieTicketBooking.enums;
using MovieTicketBooking.models;
using MovieTicketBooking.repository;
using MovieTicketBooking.service;
using MovieTicketBooking.strategy.locking;

static Show CreateShow()
{
    Movie movie = new Movie("movie-1", "The Matrix", 136);
    Theatre theatre = new Theatre("theatre-1", "Downtown Cinema");
    Screen screen = new Screen("screen-1");
    screen.AddSeat(new RegularSeat("A1", 12.50));
    screen.AddSeat(new RegularSeat("A2", 12.50));
    screen.AddSeat(new ReclinerSeat("B1", 20.00));
    theatre.AddScreen(screen);

    return new Show(
        "show-1",
        movie,
        DateTime.Now.AddHours(2),
        DateTime.Now.AddHours(4),
        screen,
        theatre);
}

static void PrintBookingDetails(Booking booking)
{
    Console.WriteLine($"Booking ID: {booking.BookingId}");
    Console.WriteLine($"User ID: {booking.UserId}");
    Console.WriteLine($"Show ID: {booking.ShowId}");
    Console.WriteLine($"Seats: {string.Join(", ", booking.SeatIds)}");
    Console.WriteLine($"Status: {booking.Status}");
    Console.WriteLine($"Payment type: {booking.PaymentType}");
    Console.WriteLine($"Amount: {booking.Amount:C}");
}

Show show = CreateShow();

Console.WriteLine("Normal booking");
using (InMemoryLockProvider lockProvider = new InMemoryLockProvider())
{
    BookingRepository bookingRepository = new BookingRepository();
    BookingService bookingService = new BookingService(bookingRepository, lockProvider);

    Booking booking = bookingService.CreateBooking("user-1", show, new List<string> { "A1", "A2" });
    Console.WriteLine("Created booking:");
    PrintBookingDetails(booking);

    bookingService.ConfirmBooking(booking, PaymentType.CARD);
    Console.WriteLine("Confirmed booking:");
    PrintBookingDetails(booking);
}

Console.WriteLine();
Console.WriteLine("Concurrent booking attempt");
using (InMemoryLockProvider concurrentLockProvider = new InMemoryLockProvider())
{
    BookingRepository bookingRepository = new BookingRepository();
    BookingService bookingService = new BookingService(bookingRepository, concurrentLockProvider);
    List<string> requestedSeats = new List<string> { "B1" };

    Task<Booking> firstAttempt = Task.Run(() =>
        bookingService.CreateBooking("user-2", show, requestedSeats));
    Task<Booking> secondAttempt = Task.Run(() =>
        bookingService.CreateBooking("user-3", show, requestedSeats));

    (string UserId, Task<Booking> Attempt)[] attempts = new[]
    {
        ("user-2", firstAttempt),
        ("user-3", secondAttempt)
    };
    await Task.WhenAll(attempts.Select(attempt => attempt.Attempt.ContinueWith(_ => { })));

    foreach ((string UserId, Task<Booking> Attempt) attempt in attempts)
    {
        if (attempt.Attempt.IsCompletedSuccessfully)
        {
            Console.WriteLine("Successful concurrent booking:");
            PrintBookingDetails(attempt.Attempt.Result);
        }
        else
        {
            Console.WriteLine($"Booking attempt failed for {attempt.UserId}: {attempt.Attempt.Exception?.GetBaseException().Message}");
        }
    }
}
