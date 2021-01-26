using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
