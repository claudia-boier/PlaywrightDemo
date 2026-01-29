using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightDemo;

public class NUnitPlaywright: PageTest
{
    private LoginPageUpgraded _loginPageUpgraded;

    [SetUp]
    public void Setup()
    {
        _loginPageUpgraded = new LoginPageUpgraded(Page);
    }

    [Test]
    [TestCaseSource(nameof(Login))]
    public async Task Test2(LoginModel loginModel)
    {
        // Page.SetDefaultTimeout(10);
        await Page.GotoAsync("http://www.eaapp.somee.com", new PageGotoOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded
        });
        
        await _loginPageUpgraded.ClickLogin();
        await _loginPageUpgraded.Login(loginModel.Username, loginModel.Password);
        Assert.That(await _loginPageUpgraded.EmployeeDetailsExists(), Is.True);

        await Expect(Page.Locator("text=Employee Details")).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions
        {
            Timeout = 10
        });
    }
    
    public static IEnumerable<LoginModel> Login()
    {
        yield return new LoginModel() { Username = "admin", Password = "password" };
    }
        
    [Test]
    public async Task TestNetwork()
    {
        await Page.GotoAsync("http://www.eaapp.somee.com");
        
        await _loginPageUpgraded.ClickLogin();
        await _loginPageUpgraded.Login("admin", "password");

        // var waitResponse = Page.WaitForResponseAsync("**/Employee");
        // await _loginPageUpgraded.ClickEmployeeList();
        // var getResponse = await waitResponse;
        // System.Console.WriteLine(getResponse);
        
        var response = await Page.RunAndWaitForResponseAsync( () => _loginPageUpgraded.ClickEmployeeList(), x => x.Url.Contains("/Employee") && x.Status == 200);
        
        Assert.That(await _loginPageUpgraded.EmployeeDetailsExists(), Is.True);

        await Expect(Page.Locator("text=Employee Details")).ToBeVisibleAsync();
    }

    [Test]
    public async Task TestNetworkInterception()

    
    {

        Page.Request += (_, request) => System.Console.WriteLine(request.Method + "---" + request.Url);
        Page.Response += (_, response) => System.Console.WriteLine(response.Status + "---" + response.Url);
        
        await Page.GotoAsync("https://practicesoftwaretesting.com/");

        
        
        
        // await Page.RouteAsync("**/*", async route =>
        // {
        //     if(route.Request.ResourceType == "image")
        //     {
        //         await route.AbortAsync();
        //     }
        //     else
        //     {
        //         await route.ContinueAsync();
        //     }
        // });
    }
    
      
}

