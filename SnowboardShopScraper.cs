abstract class SnowboardShopScraper
{ 
    public string SiteUrl="empty";
    public abstract int ReturnPrice(string brandName, string boardName, string makeYear); 
    public  abstract Task ScrapeSite(string brandName, string boardName, string makeYear); 
    public abstract void GoToProductPage();
} 
/*
todo:
prinvate string baseUrl; 
private void (?) goToProductPage  
*/