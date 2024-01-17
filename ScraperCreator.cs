class ScraperCreator
{
    public static SnowboardShopScraper CreateScraper(string name)
    {   switch(name)
        {
            case ("Boardhouse"):
                return new BoardhouseScraper();
            case("Supersklep"):
                return new SupersklepScraper();
            default:
                return new BoardhouseScraper();
        }
    }
}