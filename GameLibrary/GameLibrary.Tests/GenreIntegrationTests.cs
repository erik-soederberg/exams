using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GameLibrary.Tests;

// Integration test - spins up the actual API and sends a real HTTP request
public class GenreIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GenreIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllGenres_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/Genre");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}