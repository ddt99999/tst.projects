using System;
using System.Runtime.Serialization;

namespace OrderManagementEngine.Core.Utils
{
    [Serializable]
    public class EntityException : ApplicationException
    {
        public EntityException()
        { }

        public EntityException(string message)
            : base(message)
        { }

        public EntityException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected EntityException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}