using System.Text.RegularExpressions;
abstract class SnowboardShopScraper
{ 
    public string SiteUrl="empty";
    public abstract int ReturnPrice(string brandName, string boardName, string makeYear); 
    public  abstract Task ScrapeSite(string[] args); 
    public abstract void GoToProductPage();
    public void RemoveInvisibleCharacters(List<string> inputStrings)
    {

        for (int i=0; i < inputStrings.Count;i++ )
        {
            // Replace invisible characters using a regular expression
            string cleanedString = Regex.Replace(inputStrings[i], @"\p{C}", string.Empty);

            inputStrings[i] = cleanedString;
        }

    }

    public void RemoveInvisibleCharacters(string inputString)
    {
        Regex.Replace(inputString, @"\p{C}", string.Empty);
    }
} 
/*
todo:
prinvate string baseUrl; 
private void (?) goToProductPage  
*/