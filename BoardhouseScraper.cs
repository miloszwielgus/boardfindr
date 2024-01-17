using System.Text.RegularExpressions;
using Microsoft.Playwright;
class BoardhouseScraper : SnowboardShopScraper 
{ 
    public BoardhouseScraper()
    {
        SiteUrl="https://boardhouse.pl/";
    }
     public override async Task ScrapeSite(string brandName, string boardName, string makeYear)
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync();
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        string url = string.Format("https://boardhouse.pl/produkt/deska-snowboardowa-{0}-{1}-{2}/", brandName, boardName, makeYear);
        await page.GotoAsync(url);
        string pageTitle= await page.TitleAsync();

        if(pageTitle != "404: Nie znaleziono")
        {
            var priceElement = page.Locator("xpath=/html/body/div[3]/main/div/div[1]/div/div[1]/div[2]/div[1]/div[2]/div");
            string locator = string.Format("h1:has-text(\"{0} {1}\")",brandName,boardName);
            var nameElement = page.Locator(locator); 
            
            BoardDataHolder BoardData = BoardDataHolder.Instance;

            string pricesDiv = await priceElement.InnerTextAsync(); 
            string boardNameSite = await nameElement.InnerTextAsync();

            

            BoardData.AddBoardPrice(SiteUrl,SelectDiscountedPrice(pricesDiv),boardNameSite);
        }
        await browser.CloseAsync();
    }
    public override int ReturnPrice(string brandName, string boardName, string makeYear)
    {
        return 0;
    } 
    public override void GoToProductPage()
    {

    } 
    private string SelectDiscountedPrice(string prices)
    {
        var parts = prices.Split('\n'); 
        if (parts.Length >1) return  parts[1];
        return parts[0];
    }
} 
