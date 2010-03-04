using System;
using System.Runtime.Serialization;

namespace Sarabi.ApplicationServices.Contributors
{
    [Serializable]
    public class InvalidCelebrityException : Exception
    {
        public InvalidCelebrityException()
        {
        }

        public InvalidCelebrityException(string message) : base(message)
        {
        }

        public InvalidCelebrityException(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidCelebrityException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}