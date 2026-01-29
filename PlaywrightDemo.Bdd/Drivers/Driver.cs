using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

public class Driver : IDisposable
{
    private readonly Task<IPage> _page;
    private IBrowser? _browser;

    public Driver() => _page = InitialisePlaywright();

    public IPage Page => _page.Result;

    private async Task<IPage> InitialisePlaywright()
    {
        var playwright = await Playwright.CreateAsync();
        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            SlowMo = 1500
        });
        var browserContext = await _browser.NewContextAsync();
        return await browserContext.NewPageAsync();
    }

    public void Dispose() => _browser.CloseAsync();
}