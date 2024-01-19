using System.Globalization;
using System.Text.RegularExpressions;
class SiteBoardPrice : IComparable
{
    public string siteUrl{get; set;}

    public string boardName{get; set;}
    public string price{get; set;} 

    public decimal priceDecimal{get; set;}

    public SiteBoardPrice(string site,string board,string priceArg)
    {
        siteUrl = site;
        boardName = board;
        price = priceArg;
        price = Regex.Replace(price, "[^0-9,]", ""); 
        priceDecimal = decimal.Parse(price,CultureInfo.InvariantCulture);
    } 

    public int CompareTo(object ?obj)
    {
        if (obj == null)
        {
            return 1; 
        }
        SiteBoardPrice? other = obj as SiteBoardPrice;
        if(other != null) return priceDecimal.CompareTo(other.priceDecimal);
        return 0;
    }

}