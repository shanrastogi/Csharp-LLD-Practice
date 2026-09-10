using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.model;

namespace CarRental.repository
{
    public class BranchRepository
    {
        private readonly Dictionary<string, Branch> branchMap = new();
        public void AddBranch(Branch branch)
        {
            branchMap[branch.Id] = branch;
        }
        public Branch GetBranch(string id)
        {
            return branchMap[id];
        }
    }
}