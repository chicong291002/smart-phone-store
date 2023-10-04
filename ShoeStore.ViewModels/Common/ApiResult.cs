using SmartPhoneStore.ViewModels.Utilities.Slides;
using System;
using System.Collections.Generic;

namespace SmartPhoneStore.ViewModels.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }

    }
}
