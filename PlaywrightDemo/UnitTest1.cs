// using Microsoft.Playwright;
// using NUnit.Framework;

// namespace PlaywrightDemo;

// public class Tests
// {
//     [SetUp]
//     public void Setup()
//     {
//     }

//     [Test]
//     public async Task Test1()
//     {
//         using var playwright = await Playwright.CreateAsync();
//         await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
//         {
//             Headless = false,
//             SlowMo = 1500
//         });
//         await using var browserContext = await browser.NewContextAsync();
//         var page = await browserContext.NewPageAsync();

//         await page.GotoAsync("http://www.eaapp.somee.com");
//         await page.ClickAsync("text=Login");
//         await page.ScreenshotAsync(new PageScreenshotOptions
//         {
//             Path = "EApp.jpg"
//         });
//         await page.GetByLabel("UserName").FillAsync("admin");
//         await page.GetByLabel("Password").FillAsync("password");
//         await page.ClickAsync("text=Log in");
//         var isExist = await page.Locator("text=Employee Details").IsVisibleAsync();
//         Assert.That(isExist, Is.True);
//     }
// }
