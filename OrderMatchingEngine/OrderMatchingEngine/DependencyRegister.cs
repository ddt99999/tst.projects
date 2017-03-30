using System.Collections.Generic;
using System.IO;
using Disruptor;
using Disruptor.Dsl;
using Microsoft.Practices.Unity;
using OrderManagementEngine.Core.BusinessEntities;
using OrderManagementEngine.Core.CancelOrderEvents;
using OrderManagementEngine.Core.Disruptor;
using OrderManagementEngine.Core.Interfaces;
using OrderManagementEngine.Core.Messages;
using OrderManagementEngine.Core.Networks;
using OrderManagementEngine.Core.NewOrderEvents;
using OrderManagementEngine.Core.Services;
using OrderManagementEngine.Core.Utils;
using QuickFix;

namespace OrderMatchingEngine.Main
{
    public class DependencyRegister
    {
        public static void RegisterDependencies()
        {
            // FIX registration
            DIContainer.Instance.RegisterInstance(
                typeof (SessionSettings),
                new SessionSettings("./FIX.OrderMatchingEngine.cfg"),
                new ContainerControlledLifetimeManager());

            var sessionSettings = DIContainer.Instance.Resolve<SessionSettings>();
            DIContainer.Instance.RegisterInstance(
               typeof(FileStoreFactory),
               new FileStoreFactory(sessionSettings), 
               new ContainerControlledLifetimeManager());

            DIContainer.Instance.RegisterInstance(
               typeof(FileLogFactory),
               new FileLogFactory(sessionSettings), 
               new ContainerControlledLifetimeManager());

            // Services registration
            DIContainer.Instance.RegisterType<IFix44MessageHandler, Fix44MessageHandler>(
                new ContainerControlledLifetimeManager());
            DIContainer.Instance.RegisterType<IFixConnectivityHandler, FixConnectivityHandler>(
                new ContainerControlledLifetimeManager());
            DIContainer.Instance.RegisterType<IOrderMatchingService, MarketOrderMatchingService>(
                new ContainerControlledLifetimeManager());
            DIContainer.Instance.RegisterType<IStatisticRecordingService, StatisticRecordingService>(
                new ContainerControlledLifetimeManager());
            DIContainer.Instance.RegisterType<IOrderBookPriceDisplayService, OrderBookPriceDisplayService>(
                new ContainerControlledLifetimeManager());
            DIContainer.Instance.RegisterType<IRingBufferCaller<NewOrderEvent>, NewOrderEventRingBufferCaller>(
                new ContainerControlledLifetimeManager());
            DIContainer.Instance.RegisterType<IEventTranslatorTwoArg<NewOrderEvent, Order, SessionID>, NewOrderTranslator>(
                new ContainerControlledLifetimeManager());

            DIContainer.Instance.RegisterType<IRingBufferCaller<CancelOrderEvent>, CancelOrderEventRingBufferCaller>(
                new ContainerControlledLifetimeManager());
            DIContainer.Instance.RegisterType<IEventTranslatorTwoArg<CancelOrderEvent, Order, SessionID>, CancelOrderEventTranslator>(
                new ContainerControlledLifetimeManager());


            // Market Orderbook initialization
            DIContainer.Instance.RegisterType<IOrderBookFactory, OrderBookFactory>(
                new ContainerControlledLifetimeManager());
            DIContainer.Instance.RegisterType<IOrderBookRepository, OrderBookRepository>(
                new ContainerControlledLifetimeManager());
            DIContainer.Instance.RegisterType<IMarketBookFactory, MarketBookFactory>(
                new ContainerControlledLifetimeManager());

            DIContainer.Instance.RegisterInstance(
                typeof(IDictionary<Asset, OrderBook>),
                DIContainer.Resolve<IMarketBookFactory>().CreateMarketBook(),
                new ContainerControlledLifetimeManager());

            DIContainer.Instance.RegisterInstance(
                typeof (IdGenerator),
                "ExecIdGenerator",
                new IdGenerator(),
                new ContainerControlledLifetimeManager());
            DIContainer.Instance.RegisterInstance(
                typeof (IdGenerator),
                "OrderIdGenerator",
                new IdGenerator(),
                new ContainerControlledLifetimeManager());

            // Disruptor Event Handler registration
            DIContainer.Instance.RegisterType<IEventHandler<NewOrderEvent>, NewOrderRegistrator>(
                "NewOrderRegistrator", new TransientLifetimeManager());
            DIContainer.Instance.RegisterType<IEventHandler<NewOrderEvent>, NewOrderMatcher>(
                "NewOrderMatcher", new TransientLifetimeManager());
            DIContainer.Instance.RegisterType<IEventHandler<NewOrderEvent>, NewOrderStatisticsRecorder>(
                "NewOrderStatisticsRecorder", new TransientLifetimeManager());
            DIContainer.Instance.RegisterType<IEventHandler<NewOrderEvent>, NewOrderResponder>(
                "NewOrderResponder", new TransientLifetimeManager());

            DIContainer.Instance.RegisterType<IEventHandler<CancelOrderEvent>, CancelOrderStatisticsRecorder>(
                "CancelOrderStatisticsRecorder", new TransientLifetimeManager());
            DIContainer.Instance.RegisterType<IEventHandler<CancelOrderEvent>, CancelOrderResponder>(
                "CancelOrderResponder", new TransientLifetimeManager());

            // Disruptor registration
            DIContainer.Instance.RegisterType<IDisruptorFactory<NewOrderEvent>, DisruptorFactory<NewOrderEvent>>(
                new ContainerControlledLifetimeManager());
            DIContainer.Instance.RegisterType<IDisruptorFactory<CancelOrderEvent>, DisruptorFactory<CancelOrderEvent>>(
                new ContainerControlledLifetimeManager());
            
            var newOrderDisruptor = DIContainer.Resolve<IDisruptorFactory<NewOrderEvent>>().CreateDisruptor();

            // sequence of events to do order matching
            newOrderDisruptor.HandleEventsWith(new NewOrderPersister(new StreamWriter("../../../Log/Test.txt")));

            newOrderDisruptor
                .HandleEventsWith(DIContainer.ResolveByName<IEventHandler<NewOrderEvent>>("NewOrderRegistrator"))
                .Then(DIContainer.ResolveByName<IEventHandler<NewOrderEvent>>("NewOrderMatcher"))
                .Then(DIContainer.ResolveByName<IEventHandler<NewOrderEvent>>("NewOrderStatisticsRecorder"))
                .Then(DIContainer.ResolveByName<IEventHandler<NewOrderEvent>>("NewOrderResponder"));
                
            DIContainer.Instance.RegisterInstance(
               typeof(Disruptor<NewOrderEvent>),
               newOrderDisruptor,
               new ContainerControlledLifetimeManager());

            var cancelOrderDisruptor = DIContainer.Resolve<IDisruptorFactory<CancelOrderEvent>>().CreateDisruptor();

            // TODO Subscribe the eventhandlers for CancelOrderDisruptor

            
            var newOrderRingBuffer = newOrderDisruptor.RingBuffer;
            var cancelOrderRingBuffer = cancelOrderDisruptor.RingBuffer;
            DIContainer.Instance.RegisterInstance(
                typeof (RingBuffer<NewOrderEvent>),
                newOrderRingBuffer,
                new ContainerControlledLifetimeManager());

            DIContainer.Instance.RegisterInstance(
                typeof (RingBuffer<CancelOrderEvent>),
                cancelOrderRingBuffer,
                new ContainerControlledLifetimeManager());

            DIContainer.Instance.RegisterType<IApplication, NetworkRequestHandler>(
                new ContainerControlledLifetimeManager());

            DIContainer.Instance.RegisterType<IEngine, OrderMatchingEngine>();
        } 
    }
}