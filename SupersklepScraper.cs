using Microsoft.Playwright;
class SupersklepScraper : SnowboardShopScraper
{
     public SupersklepScraper()
    {
        SiteUrl="https://supersklep.pl/";
    }
     public override async Task ScrapeSite(string brandName, string boardName, string makeYear)
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync();
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        string url = string.Format("https://supersklep.pl/catalog/page/products?keywords={0}%20{1}", brandName, boardName, makeYear);
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