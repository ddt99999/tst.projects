using System;
using System.ServiceProcess;
using PhoenixSea.Trading.Utils.DI;
using PhoenixSea.Trading.Utils.Interfaces;

namespace PhoenixSea.Trading.MarketDataService.Bootstrappers
{
    public class Bootstrapper
    {
        public static void RunConsole()
        {
            var engine = DependencyContainer.Resolve<IApplication>();
            engine.Start();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            engine.Stop();
        }

        public static void RunWindowService()
        {
            var servicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}