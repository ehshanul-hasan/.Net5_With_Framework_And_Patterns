using System;

namespace Hahn.ApplicatonProcess.December2020.Domain.Exceptions
{
    public class BusinessExceptionBase : Exception
    {
        public int Status { get; private set; }
        public string Code { get; set; }

        public BusinessExceptionBase(int status, string message) : base(message)
        {
            Status = status;
        }
    }
}
