using System;
using System.Runtime.Serialization;

namespace Project01
{
    [Serializable]
    internal class OutOfRangeOfMenu : Exception
    {
        public OutOfRangeOfMenu()
        {
        }

        public OutOfRangeOfMenu(string message) : base(message)
        {
        }

        public OutOfRangeOfMenu(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OutOfRangeOfMenu(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}