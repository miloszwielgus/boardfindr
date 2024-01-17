using Microsoft.Playwright;
using System.Text.RegularExpressions;
class SpecifactionScraper : SnowboardShopScraper 
{ 
    public SpecifactionScraper()
    {
        SiteUrl="https://www.evo.com/";
    }
     public override async Task ScrapeSite(string brandName, string boardName, string makeYear)
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync();
        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        string url = string.Format("https://www.evo.com/outlet/snowboards/{0}-{1}-snowboard-{2}", brandName, boardName, makeYear);
        await page.GotoAsync(url);
        string pageTitle= await page.TitleAsync();
 
        if(pageTitle != "Your Page Has Been Misplaced | evo")
        {
            var element = page.Locator("a:has-text(\"Specs\")");
            await element.ClickAsync();
            
            
            var specificationTable = await page.QuerySelectorAsync(".spec-table");
            
            var resultTable = ExtractTableData(specificationTable); 

            BoardDataHolder BoardData = BoardDataHolder.Instance;
            BoardData.SetBoardSpecificationDictionary(await resultTable);
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
    
    private void RemoveInvisibleCharacters(List<string> inputStrings)
    {
        string[] cleanedStrings;

        for (int i=0; i < inputStrings.Count;i++ )
        {
            // Replace invisible characters using a regular expression
            string cleanedString = Regex.Replace(inputStrings[i], @"\p{C}", string.Empty);

            inputStrings[i] = cleanedString;
        }

    }
} 