using MVCDemo_Repository.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCDemo_Repository.Service
{
    public class Result : IResult
    {
        public Guid ID { get; private set; }

        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public List<IResult> InnerResults { get; protected set; }

        public Result(bool success)
        {
            ID = Guid.NewGuid();
            Success = success;
            InnerResults = new List<IResult>();
        }
        public Result() : this(false) { }

    }
}
