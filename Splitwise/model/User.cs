using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Splitwise.model
{
    public class User
    {
        public string UserId { get; }
        public string Name { get; }

        public User(string id, string name)
        {
            UserId = id;
            Name = name;
        }
    }
}