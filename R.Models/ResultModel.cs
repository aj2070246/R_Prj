namespace R.Models

{
    public class ResultModel<T>
    {

        public ResultModel(T model, bool isSuccess = true, string message = "با موفقیت انجام شد", int statusCode = 200)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
            Model = model;
        }
        public ResultModel(bool isSuccess = false, string message = "خطا در انجام عملیات", int statusCode = 500)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode; 
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public T Model { get; set; }
    }
}
