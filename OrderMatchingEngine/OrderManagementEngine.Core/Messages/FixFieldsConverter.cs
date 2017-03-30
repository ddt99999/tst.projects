using System;
using OrderManagementEngine.Core.BusinessEntities;
using QuickFix;
using QuickFix.Fields;

namespace OrderManagementEngine.Core.Messages
{
    public static class FixFieldsConverter
    {
        public static OrderSide Convert(Side side)
        {
            switch (side.getValue())
            {
                case Side.BUY:
                    return OrderSide.Bid;
                case Side.SELL:
                    return OrderSide.Ask;
                default:
                    throw new IncorrectTagValue(side.Tag);
            }
        }

        public static Side Convert(OrderSide side)
        {
            switch (side)
            {
                case OrderSide.Bid:
                    return new Side(Side.BUY);
                case OrderSide.Ask:
                    return new Side(Side.SELL);
                default:
                    throw new ApplicationException("Unknown MarketSide when translating " + side);
            }
        }

        public static OrderType Convert(OrdType type)
        {
            switch (type.getValue())
            {
                case OrdType.LIMIT:
                    return OrderType.Limit;
                default:
                    throw new IncorrectTagValue(type.Tag);
            }
        }

        public static OrdType Convert(OrderType type)
        {
            switch (type)
            {
                case OrderType.Limit:
                    return new OrdType(OrdType.LIMIT);
                default:
                    throw new ApplicationException("Unknown OrderType when translating " + type);
            }
        }

        public static OrderStatus Convert(OrdStatus ordStatus)
        {
            switch (ordStatus.Obj)
            {
                case ExecType.NEW:
                    return OrderStatus.New;
                case ExecType.PARTIAL_FILL:
                    return OrderStatus.Partial;
                case ExecType.FILL:
                    return OrderStatus.Filled;
                case ExecType.CANCELED:
                    return OrderStatus.Canceled;
                case ExecType.REPLACED:
                    return OrderStatus.Replaced;
                case ExecType.REJECTED:
                    return OrderStatus.Rejected;
                case ExecType.SUSPENDED:
                    return OrderStatus.Suspended;
                case ExecType.TRADE:
                    return OrderStatus.Traded;
            }
            return OrderStatus.Unknown;
        }
    }
}