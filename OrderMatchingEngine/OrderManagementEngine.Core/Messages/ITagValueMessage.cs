using OrderManagementEngine.Core.BusinessEntities;

namespace OrderManagementEngine.Core.Messages
{
    public interface ITagValueMessage
    {
        void PutInt(int val, int tag);

        void PutLong(long val, int tag);

        void PutChar(char val, int tag);

        char GetChar(int tag);

        void PutBool(bool val, int tag);

        int GetInt(int tag);

        long GetLong(int tag);

        char[] GetString(int tag);

        void PutString(char[] val, int tag);

        bool GetBool(int tag);

        int GetPrice();

        bool GetBuy();

        char[] GetSymbol();

        char[] GetClientId();

        int GetQuantity();

        ValueType GetTagValueType(int tag);

        void setOrderStatus(OrderStatus status);

        bool isFilled(int decrement);

        OrderStatus GetOrderStatus();

        bool GetIsValid();

        void validate();
    }
}