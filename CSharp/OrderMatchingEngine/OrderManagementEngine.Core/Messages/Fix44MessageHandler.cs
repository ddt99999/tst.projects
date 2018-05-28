using System.Globalization;
using Disruptor;
using OrderManagementEngine.Core.BusinessEntities;
using OrderManagementEngine.Core.Interfaces;
using OrderManagementEngine.Core.NewOrderEvents;
using OrderManagementEngine.Core.Utils;
using QuickFix;
using QuickFix.Fields;
using QuickFix.FIX44;

namespace OrderManagementEngine.Core.Messages
{
    public class Fix44MessageHandler : IFix44MessageHandler
    {
        private readonly IEventTranslatorTwoArg<NewOrderEvent, Order, SessionID> translator;
        private IRingBufferCaller<NewOrderEvent> newOrderEventRingBufferCaller;

        public Fix44MessageHandler(
            IEventTranslatorTwoArg<NewOrderEvent, Order, SessionID> translator,
            IRingBufferCaller<NewOrderEvent> newOrderEventRingBufferCaller)
        {
            this.translator = translator;
            this.newOrderEventRingBufferCaller = newOrderEventRingBufferCaller;
        }

        public void ProcessMessage(NewOrderSingle newOrder, SessionID sessionId)
        {
            var order = Convert(newOrder);
            newOrderEventRingBufferCaller.CallRingBuffer().PublishEvent(translator, order, sessionId);
        }

        public QuickFix.FIX44.Message CreateFillReport(OrderMatchReport report)
        {
            var execId = string.Format("{0}{1}", report.Asset.Symbol, DIContainer.ResolveByName<IdGenerator>("ExecIdGenerator").GenerateId());
            var symbol = new Symbol(report.Asset.Symbol);
            var executionReport = new ExecutionReport(
                new OrderID(report.OrderId.ToString(CultureInfo.InvariantCulture)),
                new ExecID(execId),
                new ExecType(report.MatchType == OrderMatchType.Filled
                                 ? ExecType.FILL
                                 : ExecType.PARTIAL_FILL),
                new OrdStatus(report.MatchType == OrderMatchType.Filled
                                  ? OrdStatus.FILLED
                                  : OrdStatus.PARTIALLY_FILLED),
                symbol,
                FixFieldsConverter.Convert(report.OrderSide),
                new LeavesQty(report.RemainingQuantity),
                new CumQty(report.OriginalOrderQuantity - report.RemainingQuantity),
                new AvgPx(report.Price))
                {
                    ClOrdID = new ClOrdID(report.ClOrdId),
                    Symbol = symbol,
                    OrderQty = new OrderQty(report.OriginalOrderQuantity),
                    LastQty = new LastQty(report.MatchedQuantity),
                    LastPx = new LastPx(report.Price)
                };

            if (TradingAccount.IsSet(report.Account))
                executionReport.SetField(new Account(report.Account.Name));

            return executionReport;
        }

        // TODO to implement this method
        public QuickFix.FIX44.Message CreateCancelReport(OrderCancelReport report)
        {
            return null;
        }

        /// <summary>
        ///  Translate FIX44 NewOrderSingle message to OrderData
        /// </summary>
        /// <param name="newOrder">The message</param>
        /// <returns>The order data</returns>
        /// <exception cref="QuickFix.IncorrectTagValue"></exception>
        private static Order Convert(NewOrderSingle newOrder)
        {
            ValidateIsSupportedOrderType(newOrder.OrdType);
            
            return Convert(MessageType.NewOrderSingle,
                           newOrder.Symbol,
                           newOrder.Side,
                           newOrder.OrdType,
                           newOrder.OrderQty,
                           newOrder.Price,
                           newOrder.ClOrdID,
                           newOrder.IsSetAccount() ? newOrder.Account : null);
        }

        private static void ValidateIsSupportedOrderType(OrdType ordType)
        {
            switch (ordType.getValue())
            {
                case OrdType.LIMIT:
                    break;
                default:
                    throw new IncorrectTagValue(ordType.Tag);
            }
        }

        private static Order Convert(MessageType commandType, 
                                      Symbol symbol,
                                      Side side,
                                      OrdType ordType,
                                      OrderQty orderQty,
                                      Price price,
                                      ClOrdID clOrdId,
                                      Account account)
        {
            switch (ordType.getValue())
            {
                case OrdType.LIMIT:
                    if (price.Obj == 0)
                        throw new IncorrectTagValue(price.Tag);

                    break;

                //case OrdType.MARKET: break;

                default:
                    throw new IncorrectTagValue(ordType.Tag);
            }
            var orderId = DIContainer.ResolveByName<IdGenerator>("OrderIdGenerator").GenerateId();
            return new Order
            {
                CommandType = commandType,
                OrderId = orderId,
                OrderSide = FixFieldsConverter.Convert(side),
                Asset = new Asset(symbol.getValue()),
                OrderType = FixFieldsConverter.Convert(ordType),
                Quantity = orderQty.getValue(),
                Price = price.getValue(),
                ClOrdId = clOrdId.getValue(),
                Account =
                        account == null
                            ? TradingAccount.None
                            : new TradingAccount(account.getValue())
            };
        }
    }
}