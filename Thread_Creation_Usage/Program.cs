using System.Diagnostics;
Console.WriteLine("This utility will run either a bunch ");
Console.WriteLine("of threads or a bunch of tasks");
Console.WriteLine("to show how much overhead each has.");
Console.WriteLine("");
Console.WriteLine("1 for 1000000 tasks");
Console.WriteLine("2 for 1000000 threads");
Console.WriteLine("______________________________");
Console.WriteLine("Enter your selection:");
string selection = Console.ReadLine();
Console.WriteLine(selection);

ManualResetEvent resetEvent = new ManualResetEvent(false);

try
{
    switch (selection)
    {
        case "1":
            {
                Start1000Tasks();
                resetEvent.Set();
                break;
            }
        case "2":
            {
                Start1000Threads();
                resetEvent.Set();
                break;
            }
        default:
            resetEvent.Set();
            break;
    }
}
catch (OutOfMemoryException)
{
    resetEvent.Set();
    Environment.Exit(0);
}
finally
{
    resetEvent.Set();
    Environment.Exit(0);
}

static void Start1000Threads()
{
    const int OneMB = 1024 * 1024;
    string? input = Console.ReadLine();
    for (int i = 0; i <= 10000000; i++)
    {
        if (input == string.Empty)
        {
            Thread t = new Thread(Start);
            Console.WriteLine("current index is {0}", i);

            Console.WriteLine("{0} Threads: {1}MB Nonpaged",
                Process.GetCurrentProcess().Threads.Count,
                Process.GetCurrentProcess().NonpagedSystemMemorySize64 / OneMB);

            Console.WriteLine("{0} Threads: {1} Handles",
                Process.GetCurrentProcess().Threads.Count,
                Process.GetCurrentProcess().HandleCount);

            Console.WriteLine("{0} Threads: {1}MB Paged",
                Process.GetCurrentProcess().Threads.Count,
                Process.GetCurrentProcess().PagedMemorySize64 / OneMB);
            Console.WriteLine("___________________________________");
        }
        else
        {
            Console.WriteLine("paused");
            Console.ReadLine();
        }
    }
}
static void Start() { Random random = new Random(); }

static void Start1000Tasks()
{
    const int OneMB = 1024 * 1024;
    string? input = Console.ReadLine();
    for (int i = 0; i <= 10000000; i++)
    {
        if (input == string.Empty)
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("current index is {0}", i);
                Console.WriteLine("Current Task ID is: {0}", 
                                                        Task.CurrentId);
                Console.WriteLine("{0}MB Nonpaged", 
                                    Process.GetCurrentProcess().NonpagedSystemMemorySize64 / OneMB);
                Console.WriteLine("{0} Handles", 
                                    Process.GetCurrentProcess().HandleCount);
                Console.WriteLine("{0}MB Paged", 
                                    Process.GetCurrentProcess().PagedMemorySize64 / OneMB);
                Console.WriteLine("___________________________________");

            });
        }
        else
        {
            Console.WriteLine("paused");
            Console.ReadLine();
        }
    };
}