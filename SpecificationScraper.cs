using Microsoft.Playwright;
using System.Text.RegularExpressions;
class SpecifactionScraper : SnowboardShopScraper 
{ 
    public SpecifactionScraper()
    {
        SiteUrl="https://www.evo.com/";
    }
     public override async Task ScrapeSite(string[] args)
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync();
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();
        
        string search;
        string year; 
        if(args[^1] == "2024") year = "";
        if(int.TryParse(args[^1],out int result))
        {
            search = string.Join("-",args[..^1]);
            if(args[^1] == "2024") year = "";
            else year = "-"+args[^1];
        }
        else 
        {
            search = string.Join("-",args);
            year = "";
        }
        string url = string.Format("https://www.evo.com/outlet/snowboards/{0}-snowboard{1}", search,year);
        await page.GotoAsync(url);
        string pageTitle= await page.TitleAsync();
 
        if(pageTitle != "Your Page Has Been Misplaced | evo")
        {
            var element = page.Locator("a:has-text(\"Specs\")");
            await element.ClickAsync();
            
            
            var specificationTable = await page.QuerySelectorAsync(".spec-table");
            
            if(specificationTable != null)
            {
                var resultTable = ExtractTableData(specificationTable); 
                 BoardDataHolder BoardData = BoardDataHolder.Instance;
                BoardData.SetBoardSpecificationDictionary(await resultTable);
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

    async Task<Dictionary<string,List<string>>> ExtractTableData(IElementHandle tableHandle)
    {
        var tableData = new Dictionary<string, List<string>>();

        // Extract column headers (th elements)
        var headers = await tableHandle.QuerySelectorAllAsync("th");
        var headerTexts = await Task.WhenAll(headers.Select(header => header.InnerHTMLAsync())); 

        var trBlocks = await tableHandle.QuerySelectorAllAsync("tr");

        var headerTextIterator = 0;
        // Extract data from each row
        foreach (var tr in trBlocks)
        {
            var rowData = await tr.QuerySelectorAllAsync("td");
            var values = await Task.WhenAll(rowData.Select(data => data.InnerHTMLAsync())); 

            List<string> valueList = values.ToList(); 
            RemoveInvisibleCharacters(valueList);

            tableData[headerTexts[headerTextIterator]] = valueList;
            // Map headers to values and add to the dictionary
            
            headerTextIterator++;
        }

        return tableData;
    }
    
    
} 