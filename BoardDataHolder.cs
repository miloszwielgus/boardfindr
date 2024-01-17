using System.Runtime.CompilerServices;

public class BoardDataHolder
{
    // Properties
    private List<SiteBoardPrice> PriceList;
    private Dictionary<string, List<string>> BoardSpecificationDictionary;

    // Singleton instance
    private static BoardDataHolder _instance;

    // Private constructor to prevent instantiation
    private BoardDataHolder()
    {
        // Default values
        PriceList = new List<SiteBoardPrice>();
        BoardSpecificationDictionary = new Dictionary<string, List<string>>();
    }

    // Public method to get the singleton instance
    public static BoardDataHolder Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BoardDataHolder();
            }
            return _instance;
        }
    } 

    public void SetBoardSpecificationDictionary(Dictionary<string, List<string>> newDictionary)
    {
        BoardSpecificationDictionary = newDictionary;
    } 
    /*public void SetPriceDictionary(Dictionary<string,(string price ,string board)> newDictionary)
    {
        PriceDictionary = newDictionary;
    }  */
    
    public void AddBoardPrice(string shopName, string price,string board)
    {
        PriceList.Add(new SiteBoardPrice(shopName,price,board));
    }

    public Dictionary<string, List<string>> GetBoardSpecificationDictonary()
    {
        return BoardSpecificationDictionary;
    } 
    /*
    public List<SiteBoardPrice> GetPriceDictonary()
    {
        return PriceList;
    } */

    public void DisplaySpecifications()
    {
        if(BoardSpecificationDictionary.Count == 0)
        {
            Console.WriteLine("Niestety, nie udało nam się znaleźć specyfikacji Twojej wymarzonej deski!");
            return;
        }

        var longestKey = 0;
        var longestValue = 0;


        foreach (var (key,values) in BoardSpecificationDictionary)
        {
            if(key.Length > longestKey) longestKey = key.Length; 

            foreach(var value in values)
            {
                if(value.Length > longestValue) longestValue = value.Length;
            }
        } 

        string  keySpacer, valueSpacer;

        foreach (var (key, values) in BoardSpecificationDictionary)
        {
            keySpacer = new string(' ',longestKey-key.Length+1);
            Console.Write($"{key}:{keySpacer}"); 
            foreach(var value in values)
            {
                valueSpacer = new string(' ',longestValue-value.Length+1);
                Console.Write(value + valueSpacer);
            } 
            Console.Write('\n');
        } 
        Console.Write('\n');
    }

    public void DisplayPrices()
    {
        if(PriceList.Count == 0)
        {
            Console.WriteLine("Niestety, Twojej wymarzonej deski nie ma w żadnym z obsługiwanych przez nas sklepów!");
        }
        foreach (var item in PriceList)
        {
            Console.WriteLine($"Sklep: {item.siteUrl}, Cena: {item.price}, Model: {item.boardName}");
        }
    }

}