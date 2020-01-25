using System;

namespace ClassifiedAds.CrossCuttingConcerns.Exceptions
{
    public class ValidationException : Exception
    {
        public static void Requires(bool expected, string errorMessage)
        {
            if (!expected)
                throw new ValidationException(errorMessage);
        }

        public ValidationException(string message) : base(message)
        {

        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
