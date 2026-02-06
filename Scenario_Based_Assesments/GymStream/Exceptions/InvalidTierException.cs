using System;
using System.Reflection.Metadata.Ecma335;

namespace GymStream.Exceptions
{
    // Custom exception for unsupported membership tier
    public class InvalidTierException : Exception
    {
        public InvalidTierException(string message) : base(message) { }
    }
}
