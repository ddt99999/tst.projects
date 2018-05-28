namespace PhoenixSea.Common.Core
{
    public interface IContextFactory<T> where T : class
    {
        T Create();
    }
}