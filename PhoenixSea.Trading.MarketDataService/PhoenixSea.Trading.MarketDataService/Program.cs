using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Easy.Logger.Interfaces;
using PhoenixSea.Trading.MarketDataService.Bootstrappers;
using PhoenixSea.Trading.Utils.DI;

namespace PhoenixSea.Trading.MarketDataService
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                //if (EventLog.SourceExists(""))
                //{
                //    EventLog.WriteEntry("",
                //        "Fatal Exception : " + Environment.NewLine +
                //        e.ExceptionObject, EventLogEntryType.Error);
                //}
                var logger = DependencyContainer.Resolve<ILogService>().GetLogger<Program>();
                logger.Error("Fatal Exception : " + Environment.NewLine + e.ExceptionObject);
            };

            DependencyRegistrar.RegisterDependencies();

            if (Environment.UserInteractive)
            {
                Bootstrapper.RunConsole();
            }
            else
            {
                Bootstrapper.RunWindowService();
            }
        }
    }
}
