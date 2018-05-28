using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Services
{
    public interface IOrderBookPriceDisplayService
    {
        void DisplayPrices(Order order, int level);
    }
}