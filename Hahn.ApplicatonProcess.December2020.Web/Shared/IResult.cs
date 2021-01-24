using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Shared
{
    public interface IResult
    {
        int Status { get; set; }
        string Message { get; set; }
        object Data { get; set; }
    }
}
