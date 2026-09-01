using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RosterVSDD.Models;

namespace RosterVSDD.Services
{
    public class InMemoryRosterService : IRosterService
    {
        private readonly ConcurrentQueue<RosterEntry> _entries = new();

        public Task AddAsync(RosterEntry entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            entry.CreatedUtc = DateTime.UtcNow;
            _entries.Enqueue(entry);
            return Task.CompletedTask;
        }

        public IReadOnlyList<RosterEntry> GetAll()
        {
            return _entries.ToArray();
        }
    }
}
