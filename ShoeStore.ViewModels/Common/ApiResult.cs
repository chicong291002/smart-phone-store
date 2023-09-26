using ShoeStore.ViewModels.Utilities.Slides;
using System;
using System.Collections.Generic;

namespace ShoeStore.ViewModels.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
    }
}
