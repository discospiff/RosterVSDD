using System.Collections.Generic;
using System.Threading.Tasks;
using RosterVSDD.Models;

namespace RosterVSDD.Services
{
    public interface IRosterService
    {
        Task AddAsync(RosterEntry entry);
        IReadOnlyList<RosterEntry> GetAll();
    }
}
