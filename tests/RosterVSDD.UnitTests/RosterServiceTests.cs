using System.Linq;
using System.Threading.Tasks;
using RosterVSDD.Models;
using RosterVSDD.Services;
using Xunit;

namespace RosterVSDD.UnitTests
{
    public class RosterServiceTests
    {
        [Fact]
        public async Task AddAsync_AddsEntry_AndGetAllReturnsIt()
        {
            var svc = new InMemoryRosterService();
            var entry = new RosterEntry
            {
                FirstName = "Jane",
                LastName = "Doe",
                Major = "Computer Science",
                FavoriteKeyboardShortcut = "Ctrl+C",
                ShortcutContext = "Copy"
            };

            await svc.AddAsync(entry);

            var all = svc.GetAll();
            Assert.Single(all);
            Assert.Equal("Jane", all.First().FirstName);
        }
    }
}
