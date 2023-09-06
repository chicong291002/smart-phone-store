using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public ApiErrorResult(string message)
        {
            IsSuccessed = false;
            Message = message;
        }
    }
}
