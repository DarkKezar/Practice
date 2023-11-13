using Microsoft.AspNetCore.Builder;
using SUT.Stock.SetUp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SUT.Stock.Controllers;

public class ControllersTest
{
    private WebApplication _app;

    [Fact]
    public async Task GetIngTest1()
    {   
        _app = await StockServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("https://localhost:5001") };

        var response = await _client.GetAsync($"api/Ingridients?page=0&count=10");
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ObjectResult>(contentString);
        var value = ((JArray)content.Value).ToList();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        Assert.True(value.Count == 10);
    }

    [Fact]
    public async Task GetIngTest2()
    {   
        _app = await StockServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("https://localhost:5001") };

        var response = await _client.GetAsync($"api/Ingridients?page=1000&count=10");
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ObjectResult>(contentString);
        var value = ((JArray)content.Value).ToList();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        Assert.True(value.Count == 0);
    }

    [Fact]
    public async Task GetTranTest1()
    {   
        _app = await StockServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("https://localhost:5001") };

        var response = await _client.GetAsync($"api/Transactions?page=0&count=1");
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ObjectResult>(contentString);
        var value = ((JArray)content.Value).ToList();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        Assert.True(value.Count == 1);
    }

    [Fact]
    public async Task GetTranTest2()
    {   
        _app = await StockServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("https://localhost:5001") };

        var response = await _client.GetAsync($"api/Transactions?page=1000&count=10");
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ObjectResult>(contentString);
        var value = ((JArray)content.Value).ToList();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        Assert.True(value.Count == 0);
    }
}
