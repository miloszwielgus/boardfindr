using System.Text.RegularExpressions;
using Microsoft.Playwright;
class LetsboardScraper : SnowboardShopScraper 
{ 
    public LetsboardScraper()
    {
        SiteUrl="https://letsboard.pl/";
    }
     public override async Task ScrapeSite(string[] args)
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync();
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        string search = string.Join("+",args);
        string url = string.Format("https://letsboard.pl/?s={0}", search);
        await page.GotoAsync(url);
        var elementNotFound = await page.Locator("h2:has-text(\"Nic nie znaleziono\")").CountAsync();

        if(elementNotFound == 0)
        {
            string cssLocator = String.Format("li.product:has-text(\"{0} {1}\")",args[0],args[1]);
           var pricesAndNames = await page.Locator(cssLocator).AllInnerTextsAsync(); 

            
            BoardDataHolder BoardData = BoardDataHolder.Instance;
            foreach(var priceAndName in pricesAndNames)
            {
                var splitValues = priceAndName.Split(new[] { "\n" }, StringSplitOptions.None);
                
                BoardData.AddBoardPrice(SiteUrl,SelectDiscountedPrice(splitValues[1]),splitValues[0]);
            }
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
        if( prices.Count(c => char.ToLower(c) == char.ToLower('z')) == 2)
        {
            return prices;
        }
        var parts = prices.Split(' '); 
        if (parts.Length >1) return  parts[1];
        return parts[0];
    }
} 
