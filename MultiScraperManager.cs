using System.Formats.Asn1;

class MultiScraperManager
{
    private List<string> Shops;  
    private List<SnowboardShopScraper> SnowboardShopScrapers; 
    private SnowboardShopScraper specificationScraper = new SpecifactionScraper();
    public MultiScraperManager()
    {
        Shops = new List<string> {"Boardhouse","Supersklep","Letsboard"}; 
        //Shops = new List<string> {"Letsboard"}; 
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

    public async Task ScrapePrices(string[] args)
    {
        foreach (SnowboardShopScraper scraper in SnowboardShopScrapers)
        {
            await scraper.ScrapeSite(args);
        }
    } 

    public async Task ScrapeSpecification(string[] args)
    {
        await specificationScraper.ScrapeSite(args);
    } 

    public async Task RunScrapers(string[] args)
    {
        if(args[0] == "-S") 
        {
            await ScrapeSpecification(args[1..]); 
            await ScrapePrices(args[1..]);
            return;
        }
        await ScrapePrices(args);
    }
}