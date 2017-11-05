using System;

namespace PhoenixSea.Common.Core.Events
{
    public class EventArgs<TData> : EventArgs
    {
        public EventArgs(TData value)
        {
            Value = value;
        }

        public TData Value { get; }
    }
}