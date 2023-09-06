using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Common
{
    public class ApiResult<T>
    {
        public ApiResult(string isSuccessed, string message)
        {
            IsSuccessed = IsSuccessed; Message = message;
            //case : false
        }

        public ApiResult(string isSuccessed, string message , T ResultObj)
        {
            IsSuccessed = IsSuccessed; Message = message;
            // case : true
        }

        public bool IsSuccessed { get; set; }

        public string Message { get; set; }

        public T ResultObj { get; set; }
    }
}
