using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using RosterVSDD.Models;
using RosterVSDD.Services;
using Xunit;

namespace RosterVSDD.IntegrationTests
{
    public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ApiIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetRoster_InitiallyEmpty_ReturnsEmptyArray()
        {
            var client = _factory.CreateClient();
            var resp = await client.GetAsync("/api/roster");
            resp.EnsureSuccessStatusCode();
            var arr = await resp.Content.ReadFromJsonAsync<RosterEntry[]>();
            Assert.NotNull(arr);
            Assert.Empty(arr);
        }

        [Fact]
        public async Task AddEntry_ViaService_GetRosterReturnsEntry()
        {
            // Add entry using the server's DI service then query the API
            using var scope = _factory.Services.CreateScope();
            var svc = scope.ServiceProvider.GetRequiredService<IRosterService>();

            var entry = new RosterEntry
            {
                FirstName = "Integration",
                LastName = "Tester",
                Major = "Testing",
                FavoriteKeyboardShortcut = "Ctrl+I",
                ShortcutContext = "Integration Test"
            };

            await svc.AddAsync(entry);

            var client = _factory.CreateClient();
            var resp = await client.GetAsync("/api/roster");
            resp.EnsureSuccessStatusCode();
            var arr = await resp.Content.ReadFromJsonAsync<RosterEntry[]>();
            Assert.NotNull(arr);
            Assert.Contains(arr, e => e.FirstName == "Integration" && e.LastName == "Tester");
        }
    }
}
