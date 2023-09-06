using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        //return Object
        public ApiSuccessResult(T resultObj)
        {
            IsSuccessed = true;
            ResultObj = resultObj;
        }

        //not return Object 
        public ApiSuccessResult()
        {
            IsSuccessed = true;
        }
    }
}
