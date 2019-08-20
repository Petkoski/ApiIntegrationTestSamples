using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Web.Api.Core.Domain.Entities;
using Xunit;

namespace Web.Api.IntegrationTests.Controllers
{
    public class PlayersControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public PlayersControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CanGetPlayers()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/api/players");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<IEnumerable<Player>>(stringResponse);
            Assert.Contains(players, p => p.FirstName == "Wayne");
            Assert.Contains(players, p => p.FirstName == "Mario");
            Assert.Contains(players, p => p.FirstName == "Jovan");
        }

        [Theory]
        [InlineData(1, "Wayne")]
        [InlineData(3, "Jovan")]
        public async Task CanGetPlayerById(int id, string firstName)
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync($"/api/players/{id}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var player = JsonConvert.DeserializeObject<Player>(stringResponse);
            Assert.Equal(id, player.Id);
            Assert.Equal(firstName, player.FirstName);
        }

        //[Fact]
        //public async Task CanAddPlayer()
        //{
        //    var data = new Player("Jovan", "Petkoski", 184, 91, new DateTime(1995, 1, 20)) { Id = 16, Created = DateTime.UtcNow };
        //    var json = JsonConvert.SerializeObject(data);
        //    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

        //    //ADD
        //    var httpResponse = await _client.PostAsJsonAsync("/api/players/", data);

        //    httpResponse.EnsureSuccessStatusCode();

        //    var stringResponse = await httpResponse.Content.ReadAsStringAsync();
        //    var player = JsonConvert.DeserializeObject<Player>(stringResponse);
        //}
    }
}