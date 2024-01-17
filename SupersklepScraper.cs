using System.Runtime.InteropServices.Marshalling;
using Microsoft.Playwright;
class SupersklepScraper : SnowboardShopScraper
{
     public SupersklepScraper()
    {
        SiteUrl="https://supersklep.pl/";
    }
     public override async Task ScrapeSite(string[] args)
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync();
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        string search;
        if(int.TryParse(args[^1],out int result))
        {
            search = string.Join(" ",args[..^1]);
        }
        else
        {
          search = string.Join(" ",args);
        }
        string url = string.Format("https://supersklep.pl/catalog/page/products?keywords={0}", search);
        await page.GotoAsync(url);
        var elementNotFound = await page.Locator("h1:has-text(\"Nie znaleziono żadnych produktów\")").CountAsync();
 
        if(elementNotFound == 0)
        {
            var pricesAndNames = await page.Locator("div.description").AllInnerTextsAsync(); 

            
            BoardDataHolder BoardData = BoardDataHolder.Instance;

            foreach(var priceAndName in pricesAndNames)
            {
                var splitValues = priceAndName.Split(new[] { "\n\n" }, StringSplitOptions.None);
                BoardData.AddBoardPrice(SiteUrl,splitValues[0],splitValues[1]);
            }
           // string pricesDiv = await element.InnerTextAsync(); 

        }
        //BoardData.AddBoardPrice(SiteUrl,"s");

        await browser.CloseAsync();
    }
    public override int ReturnPrice(string brandName, string boardName, string makeYear)
    {
        return 0;
    } 
    public override void GoToProductPage()
    {

    } 
    
}