using System.Text.RegularExpressions;
using Microsoft.Playwright;
class BoardhouseScraper : SnowboardShopScraper 
{ 
    public BoardhouseScraper()
    {
        SiteUrl="https://boardhouse.pl/";
    }
     public override async Task ScrapeSite(string[] args)
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync();
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        string search = string.Join("+",args);
        string url = string.Format("https://boardhouse.pl/?s={0}&post_type=product&dgwt_wcas=1", search);
        await page.GotoAsync(url);
        var elementNotFound = await page.Locator("p:has-text(\"Nie znaleziono produktów, których szukasz.\")").CountAsync();

        if(elementNotFound == 0)
        {
           var pricesAndNames = await page.Locator("div.px-5").AllInnerTextsAsync(); 

            
            BoardDataHolder BoardData = BoardDataHolder.Instance;
            foreach(var priceAndName in pricesAndNames)
            {
                var splitValues = priceAndName.Split(new[] { "\n" }, StringSplitOptions.None);
                splitValues[1]=splitValues[1].Replace( '\u00A0'.ToString(), " "); 
                splitValues[1]=splitValues[1].Replace( ",", String.Empty);
                splitValues[1]=splitValues[1].Replace( ".", ","); 
                BoardData.AddBoardPrice(SiteUrl,splitValues[0],SelectDiscountedPrice(splitValues[1]));
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
        var parts = prices.Split(' '); 
        if (parts.Length >1) return  parts[1];
        return parts[0];
    }
} 
