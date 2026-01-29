using System.Runtime.InteropServices;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.Json;

namespace PlaywrightDemo;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    // [Test]
    // public async Task Test1()
    // {
    //     using var playwright = await Playwright.CreateAsync();
    //     await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
    //     {
    //         Headless = false,
    //         SlowMo = 1500
    //     });
    //     await using var browserContext = await browser.NewContextAsync();
    //     var page = await browserContext.NewPageAsync();

    //     await page.GotoAsync("http://www.eaapp.somee.com");
    //     await page.ClickAsync("text=Login");
    //     await page.ScreenshotAsync(new PageScreenshotOptions
    //     {
    //         Path = "EApp.jpg"
    //     });
    //     await page.GetByLabel("UserName").FillAsync("admin");
    //     await page.GetByLabel("Password").FillAsync("password");
    //     await page.ClickAsync("text=Log in");
    //     var isExist = await page.Locator("text=Employee Details").IsVisibleAsync();
    //     Assert.That(isExist, Is.True);
    // }
    // [Test]
   
    // public async Task GetBookApi()
    // {
    //     using var playwright = await Playwright.CreateAsync();
    //     await using var requestContext = await playwright.APIRequest.NewContextAsync(new()
    //     {
    //         BaseURL = "https://api.practicesoftwaretesting.com/"
    //     });

    //     var cart = await requestContext.GetAsync("/carts/01kg4zaastpbzprtk05d3v84sw");
    //     var cartJsonResponse = await cart.JsonAsync();

    //     System.Console.WriteLine(cartJsonResponse);
    // }

    [Test]
    public async Task PostAPIAuth()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var requestContext = await playwright.APIRequest.NewContextAsync(new ()
        {
            BaseURL = "https://simple-books-api.click"
        });

        var response = await requestContext.PostAsync("/api-clients/", new APIRequestContextOptions ()
        {
            DataObject = new
            {
                clientName = "kiwi245fdgfdgdfdshrth",
                clientEmail = "kiwi254fdgdffdstrytryf5@random.com"
            }
        });

        var responseJson = await response.JsonAsync();
        System.Console.WriteLine(responseJson);

        var token = responseJson?.GetProperty("accessToken").ToString();

        System.Console.WriteLine(token);

        Assert.That(token, Is.Not.Empty);     

        var authenticationResponse = responseJson?.Deserialize<Authenticate>(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }); 

        Assert.That(authenticationResponse?.AccessToken, Is.Not.Empty);    

    }

    public async Task<string?> GetToken()
    {
        var playwright = await Playwright.CreateAsync();
        var requestContext = await playwright.APIRequest.NewContextAsync(new ()
        {
            BaseURL = "https://simple-books-api.click"
        });

        var response = await requestContext.PostAsync("/api-clients/", new APIRequestContextOptions ()
        {
            DataObject = new
            {
                clientName = "kiwi245fdgfdgdfdshrth",
                clientEmail = "kiwi254fdgdffdstrytryf5@random.com"
            }
        });

        var responseJson = await response.JsonAsync();

        return responseJson?.Deserialize<Authenticate>(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })?.AccessToken; 

    }

    [Test]
    public async Task PostOrderUsingToken()
    {
        using var playwright = await Playwright.CreateAsync();
        await using var requestContext = await playwright.APIRequest.NewContextAsync(new ()
        {
            BaseURL = "https://simple-books-api.click"
        });

        var response = await requestContext.PostAsync("/orders", new APIRequestContextOptions ()
        {
            Headers = new Dictionary<string, string>
            {
                {"Authorization", $"Bearer {GetToken()}"}
            },
            DataObject = new
            {
                bookId = 10,
                customerName = "Kiwi"
            }
        });
    }

    public class Authenticate
    {
        public string? AccessToken { get; set; }
    }

}