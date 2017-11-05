namespace PhoenixSea.Common.Core.Models
{
    public class SubscriptionMessage
    {
        public SubscriptionMessage(byte[] topic, byte[] data)
        {
            Topic = topic;
            Data = data;
        }

        public byte[] Topic { get; }
        public byte[] Data { get; }
    }
}