using System.Threading.Tasks;
using Xunit;

namespace MinimalApiServer.Tests;

public class weatherforecast : IClassFixture<MinimalApiApplicationFactory>
{
    private readonly MinimalApiApplicationFactory factory;

    public weatherforecast(MinimalApiApplicationFactory factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task Get_Weatherforecast()
    {
        //await using var application = new MinimalApiApplicationFactory();
        //using var client = application.CreateClient();
        //var response = await client.GetStringAsync("/weatherforecast");

        using var client = factory.CreateClient();
        var response = await client.GetStringAsync("/weatherforecast");

        Assert.NotNull(response);
        //Assert.Equal("test", response.Replace("\"", ""));
    }


    [Fact]
    public async Task Get_Weatherforecast2()
    {
        //await using var application = new MinimalApiApplicationFactory();
        //using var client = application.CreateClient();
        //var response = await client.GetStringAsync("/weatherforecast");

        using var client = factory.CreateClient();
        var response = await client.GetStringAsync("/weatherforecast");

        Assert.NotNull(response);
        //Assert.Equal("test", response.Replace("\"", ""));
    }
}
