using System;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualBasic;

class Program
{
    /*static async Task Main(string[] args)
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync();
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        await page.GotoAsync("https://oxylabs.io/blog/playwright-web-scraping");

        var title = await page.TitleAsync();
        Console.WriteLine($"Page title: {title}");

        // Add your scraping logic here

        await browser.CloseAsync();
    } */

    public static async Task Main(string[] args)
    {
        await CommandLineInterface.Run(args);
    }
    /*+
    todo: 
    osobna klasa na commandline interface 
    */
}
