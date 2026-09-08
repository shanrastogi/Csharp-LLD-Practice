using enums;
using model;
using service;

public class Program
{
    public static void Main(string[] args)
    {
        RateLimiterService rateLimiterService = new RateLimiterService();

        User freeUser = new User("user1", UserTier.FREE); // 10 req in 60 sec
        User premiumUser = new User("user2", UserTier.PREMIUM); // 100 req in 60 sec

        Console.WriteLine("=== Free User Requests ===");
        for (int i = 1; i <= 15; i++)
        {
            bool allowed = rateLimiterService.AllowRequest(freeUser);
            Console.WriteLine($"Request {i} for Free User: {(allowed ? "ALLOWED" : "BLOCKED")}");
            Thread.Sleep(100); // simulate delay between requests
        }

        Console.WriteLine("\n=== Premium User Requests ===");
        for (int i = 1; i <= 120; i++)
        {
            bool allowed = rateLimiterService.AllowRequest(premiumUser);
            Console.WriteLine($"Request {i} for Premium User: {(allowed ? "ALLOWED" : "BLOCKED")}");
            Thread.Sleep(100);
        }
    }
}