using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Services
{
    public interface IStatisticRecordingService
    {
        void IncrementTotalFilledOrder(Asset asset);
        void IncrementTotalCanceledOrder(Asset asset);
        void ResetStatistics(Asset asset);
    }
}