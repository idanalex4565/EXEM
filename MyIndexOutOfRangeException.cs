using System;
using System.Runtime.Serialization;

namespace exem
{
    [Serializable]
    internal class MyIndexOutOfRangeException : Exception
    {
        public MyIndexOutOfRangeException()
        {
        }

        public MyIndexOutOfRangeException(string message) : base(message)
        {
        }

        public MyIndexOutOfRangeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MyIndexOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}