using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementPanel_.Model.ComplexType
{
    public class Result
    {
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public bool Status { get; set; }
    }
    public class Result<T> : Result
    {
        public List<T> ObjectResult { get; set; }
        public T Object { get; set; }
    }
}
