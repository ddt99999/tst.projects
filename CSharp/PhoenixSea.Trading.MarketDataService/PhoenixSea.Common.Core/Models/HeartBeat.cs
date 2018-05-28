using System;
using System.ServiceModel;
using ProtoBuf;

namespace PhoenixSea.Common.Core.Models
{
    [ProtoContract(Name = "HeartBeat")]
    [Serializable]
    public class HeartBeat : IExtensible, ITcpMessage
    {
        private StatusCode _code;
        private long _ticks;
        private IExtension extensionObject;

        [ProtoMember(1, DataFormat = DataFormat.TwosComplement, IsRequired = true, Name = "ticks")]
        public long Ticks
        {
            get { return _ticks; }
            set { _ticks = value; }
        }

        [ProtoMember(2, DataFormat = DataFormat.TwosComplement, IsRequired = true, Name = "code")]
        public StatusCode Code
        {
            get { return _code; }
            set { _code = value; }
        }

        public IExtension ExtensionObject
        {
            get
            {
                return extensionObject;
            }

            set
            {
                extensionObject = value;
            }
        }

        public IExtension GetExtensionObject(bool createIfMissing)
        {
            return Extensible.GetExtensionObject(ref this.extensionObject, createIfMissing);
        }
    }
}