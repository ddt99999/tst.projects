namespace OrderManagementEngine.Core.BusinessEntities
{
    // Asset here means stock, it can be extended to other asset class like future, option
    public class Asset
    {
        public string Symbol { get; private set; }

        public Asset(string symbol)
        {
            Symbol = symbol;
        }

        public override bool Equals(object other)
        {
            var obj = other as Asset;

            if (obj == null)
                return false;

            return Symbol == obj.Symbol;
        }

        public override int GetHashCode()
        {
            return Symbol.GetHashCode();
        }

        public override string ToString()
        {
            return Symbol;
        }
    }
}