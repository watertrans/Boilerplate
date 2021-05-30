using System;
using System.Runtime.Serialization;

namespace WaterTrans.Boilerplate.Persistence.Exceptions
{
    public class DuplicateKeyException : Exception, ISerializable
    {
        public DuplicateKeyException()
        {
        }

        public DuplicateKeyException(string message)
            : base(message)
        {
        }

        public DuplicateKeyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DuplicateKeyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
