class ScraperCreator
{
    public static SnowboardShopScraper CreateScraper(string name)
    {   switch(name)
        {
            case ("Boardhouse"):
                return new BoardhouseScraper();
            case("Supersklep"):
                return new SupersklepScraper();
            case("Letsboard"):
                return new LetsboardScraper();
            default:
                return new BoardhouseScraper();
        }
    }
}