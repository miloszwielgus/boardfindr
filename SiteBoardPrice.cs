class SiteBoardPrice
{
    public string siteUrl{get; set;}

    public string boardName{get; set;}
    public string price{get; set;}

    public SiteBoardPrice(string site,string board,string priceArg)
    {
        siteUrl = site;
        boardName = board;
        price = priceArg;
    } 

}