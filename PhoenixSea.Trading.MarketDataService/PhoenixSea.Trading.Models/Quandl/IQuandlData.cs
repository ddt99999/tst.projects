namespace PhoenixSea.Trading.Models.Quandl
{
    public interface IQuandlData<T>
    {
        T DataSet { get; set; }
    }
}