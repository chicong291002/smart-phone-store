namespace ShoeStore.ViewModels.Common
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
