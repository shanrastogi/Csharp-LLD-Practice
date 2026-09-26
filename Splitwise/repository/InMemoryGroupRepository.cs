using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Splitwise.model;

namespace Splitwise.repository
{
    public class InMemoryGroupRepository : IGroupRepository
    {
        private readonly Dictionary<string, Group> _store = new();

        public Group? FindById(string id)
        {
            return _store.TryGetValue(id, out var group) ? group : null;
        }

        public void Save(Group group)
        {
            _store[group.Id] = group;
        }
    }
}