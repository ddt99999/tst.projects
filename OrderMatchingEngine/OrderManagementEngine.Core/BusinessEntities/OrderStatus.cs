namespace OrderManagementEngine.Core.BusinessEntities
{
    public enum OrderStatus
    {
        Unknown,
        Filled,
        Partial,
        New,
        Canceled,
        Replaced,
        Rejected,
        Suspended,
        Traded
    }
}