using Microsoft.AspNetCore.Builder;
using SUT.Cafe.SetUp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Cafe.Application.OperationResult;
using Cafe.Application.UseCases.DishCases.Get;
using System.Net;

namespace SUT.Cafe.Controllers;

public class DishControllerTests
{
    private WebApplication _app;

    [Fact]
    public async Task GetBillTest1()
    {   
        _app = await CafeServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };

        var response = await _client.GetAsync($"api/Bill?page=0&count=1");
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ObjectResult>(contentString);
        var value = ((JArray)content.Value).ToList();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        Assert.True(value.Count == 1);
    }

    [Fact]
    public async Task GetBillTest2()
    {   
        _app = await CafeServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };

        var response = await _client.GetAsync($"api/Bill?page=1000&count=5");
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ObjectResult>(contentString);
        var value = ((JArray)content.Value).ToList();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        Assert.True(value.Count == 0);
    }

    [Fact]
    public async Task GetBillTest3()
    {   
        _app = await CafeServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };

        var response = await _client.GetAsync($"api/Bill/ed0090a9-448f-4184-a18e-8dd3fa443517");
        var contentString = await response.Content.ReadAsStringAsync();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetBillTest4()
    {   
        _app = await CafeServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };

        var response = await _client.GetAsync($"api/Bill/e155afc9-bc80-4de2-bf2c-b4b75298bced");
        var contentString = await response.Content.ReadAsStringAsync();

        Assert.Equal(response.StatusCode, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetDishTest1()
    {   
        _app = await CafeServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };

        var response = await _client.GetAsync($"api/Dish?page=0&count=5");
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ObjectResult>(contentString);
        var value = ((JArray)content.Value).ToList();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        Assert.True(value.Count == 5);
    }

    [Fact]
    public async Task GetDishTest2()
    {   
        _app = await CafeServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };

        var response = await _client.GetAsync($"api/Dish?page=1000&count=5");
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ObjectResult>(contentString);
        var value = ((JArray)content.Value).ToList();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        Assert.True(value.Count == 0);
    }

    [Fact]
    public async Task GetDishTest3()
    {   
        _app = await CafeServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };

        var response = await _client.GetAsync($"api/Dish/e155afc9-bc80-4de2-bf2c-b4b75298bcec");
        var contentString = await response.Content.ReadAsStringAsync();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetDishTest4()
    {   
        _app = await CafeServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };

        var response = await _client.GetAsync($"api/Dish/e155afc9-bc80-4de2-bf2c-b4b75298bced");
        var contentString = await response.Content.ReadAsStringAsync();

        Assert.Equal(response.StatusCode, HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetEmployeeTest1()
    {   
        _app = await CafeServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };

        var response = await _client.GetAsync($"api/Employee?page=0&count=1");
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ObjectResult>(contentString);
        var value = ((JArray)content.Value).ToList();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        Assert.True(value.Count == 1);
    }

    [Fact]
    public async Task GetEmployeeTest2()
    {   
        _app = await CafeServiceSetup.GetApp();
        var _client = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };

        var response = await _client.GetAsync($"api/Employee?page=1000&count=5");
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<ObjectResult>(contentString);
        var value = ((JArray)content.Value).ToList();

        Assert.Equal(response.StatusCode, HttpStatusCode.OK);
        Assert.True(value.Count == 0);
    }
}
