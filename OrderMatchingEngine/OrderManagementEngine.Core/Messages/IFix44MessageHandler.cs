using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Messages
{
    public interface IFix44MessageHandler : IFixMessageHandler
    {
        QuickFix.FIX44.Message CreateFillReport(OrderMatchReport report);
        QuickFix.FIX44.Message CreateCancelReport(OrderCancelReport report);
    }
}