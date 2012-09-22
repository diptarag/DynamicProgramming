using System;

namespace DynamicProgramming
{
    class DynamicProgrammingException : Exception
    {
        public DynamicProgrammingException() : base() { }
        public DynamicProgrammingException(string message) : base(message) { }
        public DynamicProgrammingException(string message, Exception innerException) : base(message, innerException) { }
        public DynamicProgrammingException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
