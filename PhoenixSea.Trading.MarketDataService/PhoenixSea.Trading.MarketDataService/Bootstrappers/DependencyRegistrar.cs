using System;
using System.IO;
using Easy.Logger;
using Easy.Logger.Interfaces;
using PhoenixSea.Common.Data.EF;
using PhoenixSea.Trading.Core.Services;
using PhoenixSea.Trading.MarketDataService.Services;
using PhoenixSea.Trading.NoSql.Redis;
using PhoenixSea.Trading.SqlServer.MarketData.Repositories;
using PhoenixSea.Trading.Utils.DI;
using PhoenixSea.Trading.Utils.Interfaces;
using PhoenixSea.Trading.Utils.Serialization;
using Unity.Injection;
using Unity.Lifetime;

namespace PhoenixSea.Trading.MarketDataService.Bootstrappers
{
    public class DependencyRegistrar
    {
        public static void RegisterDependencies()
        {
            var loggingService = Log4NetService.Instance;
            var configFile = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configs\Logging.config"));
            loggingService.Configure(configFile);
            DependencyContainer.RegisterInstance<ILogService>(loggingService, new ContainerControlledLifetimeManager());

            DependencyContainer.Register<IDbContextScopeFactory, DbContextScopeFactory>(new ContainerControlledLifetimeManager(), new InjectionConstructor(new InjectionParameter<IDbContextFactory>(null)));
            DependencyContainer.Register<IAmbientDbContextLocator, AmbientDbContextLocator>(new ContainerControlledLifetimeManager());

            DependencyContainer.Register<IAssetProductRepository, AssetProductRepository>(new ContainerControlledLifetimeManager());
            DependencyContainer.Register<IUsTreasuryDataRepository, UsTreasuryDataRepository>(new ContainerControlledLifetimeManager());

            DependencyContainer.Register<IUsTreasuryRepository, UsTreasuryRepository>(new ContainerControlledLifetimeManager(), new InjectionConstructor("localhost:6379"));

            DependencyContainer.Register<IJsonSerializer, JsonSerializer>(new ContainerControlledLifetimeManager());

            DependencyContainer.Register<IUsTreasuryDataService, UsTreasuryDataService>(new ContainerControlledLifetimeManager());

            DependencyContainer.Register<IDataDownloadService, DataDownloadService>(new ContainerControlledLifetimeManager());
            DependencyContainer.Register<IDataProcessingService, DataProcessingService>(new ContainerControlledLifetimeManager());
            //DependencyContainer.Register(typeof(IMarketDataService<>), typeof(DataProcessingService), new ContainerControlledLifetimeManager());

            DependencyContainer.Register<IUsTreasuryMarketDataService, UsTreasuryMarketDataService>(new ContainerControlledLifetimeManager());
            DependencyContainer.Register<IHangSengStockMarketDataService, HangSengStockYahooMarketDataService>(new ContainerControlledLifetimeManager());

            DependencyContainer.Register<IApplication, MarketDataServiceApp>(new ContainerControlledLifetimeManager());
        }
    }
}