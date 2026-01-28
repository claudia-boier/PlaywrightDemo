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
    public async Task Test2()
    {
        // Page.SetDefaultTimeout(10);
        await Page.GotoAsync("http://www.eaapp.somee.com", new PageGotoOptions
        {
            WaitUntil = WaitUntilState.DOMContentLoaded
        });
        
        await _loginPageUpgraded.ClickLogin();
        await _loginPageUpgraded.Login("admin", "password");
        Assert.That(await _loginPageUpgraded.EmployeeDetailsExists(), Is.True);

        await Expect(Page.Locator("text=Employee Details")).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions
        {
            Timeout = 10
        });
        
      
    }
}
