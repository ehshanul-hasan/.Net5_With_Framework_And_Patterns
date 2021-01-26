using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Exceptions
{
    public class NotFoundException : BusinessExceptionBase
    {
        public NotFoundException(string message) : base(404, message)
        {

        }
    }
}
