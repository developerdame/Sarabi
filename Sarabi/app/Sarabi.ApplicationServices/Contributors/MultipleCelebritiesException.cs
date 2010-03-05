using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Sarabi.ApplicationServices.Contributors
{
    [Serializable]
    public class MultipleCelebritiesException : Exception
    {
       
        public MultipleCelebritiesException()
        {
        }

        public MultipleCelebritiesException(List<string> celebrityUrlResultsFound, string message) : base(message)
        {
            CelebrityUrls = celebrityUrlResultsFound;
        }

        public MultipleCelebritiesException(string message, Exception inner) : base(message, inner)
        {
        }

        protected MultipleCelebritiesException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public List<string> CelebrityUrls { get; set; }
    }
}