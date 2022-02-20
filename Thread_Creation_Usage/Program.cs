using System.Diagnostics;

ThreadOverhead();

static void ThreadOverhead()
{
    const Int32 OneMB = 1024 * 1024;
    using (ManualResetEvent wakeThreads = new ManualResetEvent(false))
    {
        string pause = "";

        while (true)
        {
            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i <= 1000; i++)
                {
                    
                    if (pause != String.Empty)
                    {
                        wakeThreads.Set();
                        Debugger.Break();
                    }
                    Console.WriteLine("current thread is thread number {0}", i);

                    Console.WriteLine("{0}: {1}MB Nonpaged", ++i, Process.GetCurrentProcess().NonpagedSystemMemorySize64 / OneMB);
                    Console.WriteLine("{0}: {1} Handles", ++i, Process.GetCurrentProcess().HandleCount );
                    Console.WriteLine("{0}: {1}MB Paged", ++i, Process.GetCurrentProcess().PagedMemorySize64);
                    Console.WriteLine("___________________________________");

                    pause = Console.ReadLine();
                }
            });
        }        
    };
}