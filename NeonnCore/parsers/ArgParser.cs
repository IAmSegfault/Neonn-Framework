using NLog;

namespace NeonnCore.parsers;

public class ArgParser
{
    public String BundlePath;

    public ArgParser(string[] args)
    {

        Logger log = LogManager.GetCurrentClassLogger();
        
        
        if (args.Length == 0)
        {
            log.Fatal("No game bundle supplied to the executable.");
            //TODO: This should set some kind of no game variable instead of exiting eventually. Like the no game screen in LOVE.
            Environment.Exit(1);
        }
        
        else
        {
            BundlePath = args[0];
            
            if (args.Length > 1)
            {
                
            }
        }
    }
}