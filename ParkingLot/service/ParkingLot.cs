using System;
using System.Collections.Concurrent;
using enums;
using factory;
using model;
using strategy.pricing;

namespace service
{
    public class ParkingLot
    {
        private static readonly ParkingLot _instance = new ParkingLot();
        public static ParkingLot Instance => _instance;

        private readonly ConcurrentDictionary<string, ParkingFloor> _floors = new();
        private readonly ConcurrentDictionary<string, Ticket> _activeTickets = new();

        public IPricingStrategy PricingStrategy { get; set; }

        private ParkingLot()
        {
            PricingStrategy = PricingStrategyFactory.Get(PricingStrategyType.HourlyRate);
        }

        public void AddFloor(ParkingFloor floor) { _floors.TryAdd(floor.Id, floor); }

        public Ticket? ParkVehicle(Vehicle vehicle, DateTime entryTime)
        {
            foreach (var floor in _floors.Values)
            {
                var spot = floor.FindAvailableSpot(vehicle.Type);
                if (spot != null)
                {
                    string ticketId = Guid.NewGuid().ToString();
                    var ticket = new Ticket
                    {
                        TicketId = ticketId,
                        EntryTime = entryTime,
                        Vehicle = vehicle,
                        FloorId = floor.Id,
                        SpotId = spot.Id
                    };

                    _activeTickets.TryAdd(ticketId, ticket);
                    Console.WriteLine($"Vehicle parked. Ticket: {ticketId}");
                    return ticket;
                }
            }
            Console.WriteLine($"No spot available for vehicle type: {vehicle.Type}");
            return null;
        }

        public void UnparkVehicle(string ticketId, DateTime exitTime, PaymentMode paymentMode)
        {
            if (!_activeTickets.TryGetValue(ticketId, out var ticket))
            {
                Console.WriteLine("Invalid ticket ID.");
                return;
            }

            double fee = PricingStrategy.CalculateFee(ticket.Vehicle.Type, ticket.EntryTime, exitTime);
            var processor = new PaymentProcessor(PaymentStrategyFactory.Get(paymentMode));

            if (!processor.Pay(ticket, fee))
            {
                Console.WriteLine("Payment failed.");
                return;
            }

            if (_floors.TryGetValue(ticket.FloorId, out var floor) && floor.Spots.TryGetValue(ticket.SpotId, out var spot))
            {
                spot.Vacate();
            }

            _activeTickets.TryRemove(ticketId, out _);
            Console.WriteLine($"Vehicle exited. Fee charged: {fee}");
        }

        public void PrintStatus()
        {
            foreach (var floor in _floors.Values)
            {
                Console.WriteLine($"Floor: {floor.Id}");
                foreach (var spot in floor.Spots.Values)
                {
                    Console.WriteLine($" Spot {spot.Id} [{spot.AllowedType}] - {(spot.IsOccupied() ? "Occupied" : "Free")}");
                }
            }
        }
    }
}