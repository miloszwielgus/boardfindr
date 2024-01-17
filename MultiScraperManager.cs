using System.Formats.Asn1;

class MultiScraperManager
{
    private List<string> Shops;  
    private List<SnowboardShopScraper> SnowboardShopScrapers; 
    private SnowboardShopScraper specificationScraper = new SpecifactionScraper();
    public MultiScraperManager()
    {
        Shops = new List<string> {"Boardhouse","Supersklep"}; 
        SnowboardShopScrapers = new List<SnowboardShopScraper>();

        InitializeScrapers(); 
    }

    private void InitializeScrapers()
    {
        foreach (string shop in Shops)
        {
            SnowboardShopScrapers.Add(ScraperCreator.CreateScraper(shop));
        } 
    } 

    public async Task ScrapePrices(string brandName, string boardName, string makeYear)
    {
        foreach (SnowboardShopScraper scraper in SnowboardShopScrapers)
        {
            await scraper.ScrapeSite(brandName,boardName,makeYear);
        }
    } 

    public async Task ScrapeSpecification(string brandName, string boardName, string makeYear)
    {
        await specificationScraper.ScrapeSite(brandName,boardName,makeYear);
    } 

    public async Task RunScrapers(string[] args)
    {
        if(args[0] == "-S") 
        {
            await ScrapeSpecification(args[1],args[2],args[3]); 
            await ScrapePrices(args[1],args[2],args[3]);
            return;
        }
        await ScrapePrices(args[0],args[1],args[2]);
    }
}