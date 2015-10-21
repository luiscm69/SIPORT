
namespace Domain.Common.Exceptions
{
    using System;

    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }
    }
}
