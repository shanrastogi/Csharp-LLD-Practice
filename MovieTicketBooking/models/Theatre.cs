using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.models
{
    public class Theatre
    {
        public string Id { get; }
        public string Name { get; }
        public Dictionary<string, Screen> Screens = new();
        public Theatre(string _id, string _name)
        {
            Id = _id;
            Name = _name;
        }

        public void AddScreen(Screen screen)
        {
            Screens.Add(screen.Id, screen);
        }

        public Screen? GetScreen(string screenId)
        {
            return Screens.TryGetValue(screenId, out Screen? screen) ? screen : null;
        }
    }
}