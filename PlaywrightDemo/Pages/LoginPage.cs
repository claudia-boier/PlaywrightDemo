using Microsoft.Playwright;

public class LoginPage
{
    private readonly IPage _page;
    private readonly ILocator _lnkLogin;
    private readonly ILocator _txtUserName;
    private readonly ILocator _txtPassWord;
    private readonly ILocator _btnLogin;
    private readonly ILocator _lnkEmployeeDetails;



    public LoginPage(IPage page)
    {
        _page = page;
        _lnkLogin = _page.Locator("text=Login");
        _txtUserName = _page.GetByLabel("UserName");
        _txtPassWord = _page.GetByLabel("Password");
        _btnLogin = _page.Locator("text=Log in");
        _lnkEmployeeDetails = _page.Locator("text=Employee Details");
    }

    public async Task ClickLogin() => await _lnkLogin.ClickAsync();

    public async Task Login(string userName, string password)
    {
        await _txtUserName.FillAsync(userName);
        await _txtPassWord.FillAsync(password);
        await _btnLogin.ClickAsync();
    }

    public async Task<bool> EmployeeDetailsExists() => await _lnkEmployeeDetails.IsVisibleAsync();


}