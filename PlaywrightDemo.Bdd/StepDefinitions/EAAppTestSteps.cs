using Reqnroll;
using Microsoft.Playwright;
using System.Threading.Tasks;
using Reqnroll.Assist.Dynamic;
using NUnit.Framework;

namespace PlaywrightDemo.Bdd.Drivers;

[Binding]
public sealed class EAAppTestSteps
{
    private readonly Driver _driver;
    private readonly LoginPage _loginPage;

    public EAAppTestSteps(Driver driver)
    {
        _driver = driver;
        _loginPage = new LoginPage(_driver.Page);
    }

    [Given(@"I navigate to Application")]
    public void GivenINavigateToApplication()
    {
        _driver.Page.GotoAsync("http://www.eaapp.somee.com");
    }

    [Given("I click login link")]
    public async Task GivenIClickLoginLink()
    {
       await _loginPage.ClickLogin();
    }

    [Given("I enter following login details")]
    public async Task GivenIEnterFollowingLoginDetails(Table dataTable)
    {
        dynamic data = dataTable.CreateDynamicInstance();
        await _loginPage.Login((string)data.UserName, (string)data.Password);
    }

    [Then("I see Employee Lists")]
    public async Task ThenISeeEmployeeLists()
    {
        var isExist = await _loginPage.EmployeeDetailsExists();
        Assert.That(isExist, Is.True);
    }
}
