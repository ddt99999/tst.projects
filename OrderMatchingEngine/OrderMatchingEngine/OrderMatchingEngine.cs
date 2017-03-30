using System;
using Disruptor.Dsl;
using OrderManagementEngine.Core.CancelOrderEvents;
using OrderManagementEngine.Core.Interfaces;
using OrderManagementEngine.Core.NewOrderEvents;
using OrderManagementEngine.Core.Utils;
using QuickFix;

namespace OrderMatchingEngine.Main
{
    public class OrderMatchingEngine : IEngine
    {
        private readonly IApplication requestNetworkHandler;
        private readonly SessionSettings sessionSettings;
        private readonly FileStoreFactory fileStoreFactory;
        private readonly FileLogFactory fileLogFactory;
        private readonly ThreadedSocketAcceptor acceptor;
        private Disruptor<NewOrderEvent> inboundDisruptor;
        private Disruptor<CancelOrderEvent> outboundDisruptor;

        public OrderMatchingEngine(
            IApplication requestNetworkHandler, 
            SessionSettings sessionSettings,
            FileStoreFactory fileStoreFactory,
            FileLogFactory fileLogFactory)
        {
            this.requestNetworkHandler = requestNetworkHandler;
            this.sessionSettings = sessionSettings;
            this.fileStoreFactory = fileStoreFactory;
            this.fileLogFactory = fileLogFactory;
            this.acceptor = new ThreadedSocketAcceptor(requestNetworkHandler,
                                                      fileStoreFactory,
                                                      sessionSettings,
                                                      fileLogFactory);

        }

        public void Start()
        {
            Console.WriteLine("Order Matching Engine started...");
            acceptor.Start();
            inboundDisruptor = DIContainer.Resolve<Disruptor<NewOrderEvent>>();
            inboundDisruptor.Start();
            //outboundDisruptor = DIContainer.Resolve <Disruptor<OutputOrderEvent>>();
            //outboundDisruptor.Start();
        }

        public void Stop()
        {
            acceptor.Stop();
            inboundDisruptor.Shutdown();
            Console.WriteLine("Order Matching Engine stopped...");
            //outboundDisruptor.Shutdown();
            //requestNetworkHandler.Stop();
        }
    }
}