class CommandLineInterface
{
 
    private static bool ValidateArguments(string[] args)
    {
        bool specFlag = false; 
        int argsLength = 0;


        if(args.Length > 0 && args[0] == "-S")
        {
            specFlag = true;
        } 
        argsLength = args.Length;

        switch((specFlag,args.Length))
        {
            case (false,0):
                Console.WriteLine("Nie podałeś żadnych argumentów!");
                return false;
            case (true,>2):
                return true;
            case (false,>1):
                return true;
            default:
                System.Console.WriteLine("Zły format argumentów!");
                return false;
        }
    }
    public static async Task Run(string[] args)
    {
        if(ValidateArguments(args))
        {
            MultiScraperManager multiScraperManager = new MultiScraperManager();
            await multiScraperManager.RunScrapers(args);
            BoardDataHolder boardDataHolder = BoardDataHolder.Instance;
            if(args[0] == "-S")
            {
                boardDataHolder.DisplaySpecifications();
            }
            boardDataHolder.DisplayPrices();
        }
    }

 
}