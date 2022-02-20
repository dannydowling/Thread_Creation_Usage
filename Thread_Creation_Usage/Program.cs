using System.Diagnostics;

Console.WriteLine("1 for 1000 tasks");
Console.WriteLine("2 for 1000 threads");
Console.WriteLine("______________________________");
Console.WriteLine("Enter your selection:");
string selection = Console.ReadLine();

switch (selection)
{
    case "1":
        {
            Start1000Tasks();
            break;
        }
    case "2":
        {
            Start1000Threads();
            break;
        }
    default:
        ManualResetEvent resetEvent = new ManualResetEvent(false);
        resetEvent.Set();
        break;
}

static void Start1000Threads()
{
    const Int32 OneMB = 1024 * 1024;
    using (ManualResetEvent wakeThreads = new ManualResetEvent(false))
    {
        string pause = "";
        ProcessThread processThread = null;

        for (int i = 0; i <= 100000; i++)
        {
            Process.GetCurrentProcess().Threads.Add(processThread);
            if (pause != String.Empty)
            {
                wakeThreads.Set();
                Debugger.Break();
            }
            Console.WriteLine("current thread is thread number {0}", i);

            Console.WriteLine("{0}: {1}MB Nonpaged", ++i, Process.GetCurrentProcess().NonpagedSystemMemorySize64 / OneMB);
            Console.WriteLine("{0}: {1} Handles", ++i, Process.GetCurrentProcess().HandleCount);
            Console.WriteLine("{0}: {1}MB Paged", ++i, Process.GetCurrentProcess().PagedMemorySize64 / OneMB);
            Console.WriteLine("___________________________________");

            pause = Console.ReadLine();
        }
    }
}

static void Start1000Tasks()
{
    const Int32 OneMB = 1024 * 1024;
    using (ManualResetEvent wakeThreads = new ManualResetEvent(false))
    {
        string pause = "";

        while (true)
        {
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i <= 100000; i++)
                {

                    if (pause != String.Empty)
                    {
                        wakeThreads.Set();
                        Debugger.Break();
                    }
                    Console.WriteLine("current thread is thread number {0}", i);

                    Console.WriteLine("{0}: {1}MB Nonpaged", ++i, Process.GetCurrentProcess().NonpagedSystemMemorySize64 / OneMB);
                    Console.WriteLine("{0}: {1} Handles", ++i, Process.GetCurrentProcess().HandleCount);
                    Console.WriteLine("{0}: {1}MB Paged", ++i, Process.GetCurrentProcess().PagedMemorySize64 / OneMB);
                    Console.WriteLine("___________________________________");

                    pause = Console.ReadLine();
                }
            });
        }
    };
}