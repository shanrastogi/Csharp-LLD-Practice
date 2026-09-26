using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Splitwise.model;

namespace Splitwise.repository
{
    public interface IGroupRepository
    {
        Group? FindById(string id);
        void Save(Group group);
    }
}