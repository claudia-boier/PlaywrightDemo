using Microsoft.Playwright;

public class LoginPageUpgraded
{
    private readonly IPage _page;
    public LoginPageUpgraded(IPage page) => _page = page;   
    private ILocator _lnkLogin => _page.Locator("text=Login");
    private ILocator _txtUserName =>_page.GetByLabel("UserName");
    private ILocator _txtPassWord => _page.GetByLabel("Password");
    private ILocator _btnLogin => _page.Locator("text=Log in");
    private ILocator _lnkEmployeeDetails => _page.Locator("text=Employee Details");

    public async Task ClickLogin() => await _lnkLogin.ClickAsync();

    public async Task Login(string userName, string password)
    {
        await _txtUserName.FillAsync(userName);
        await _txtPassWord.FillAsync(password);
        await _btnLogin.ClickAsync();
    }

    public async Task<bool> EmployeeDetailsExists() => await _lnkEmployeeDetails.IsVisibleAsync();


}