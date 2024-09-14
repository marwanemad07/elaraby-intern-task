namespace OnlineShopping.Core.Dtos
{
    public class RestDto<T>
    {
        public RestDto(int statusCode)
        {
            StatusCode = statusCode;
            Message = GetMessageBasedOnStatusCode(statusCode);
        }
        public RestDto(int statusCode, T data)
        {
            StatusCode = statusCode;
            Message = GetMessageBasedOnStatusCode(statusCode);
            Data = data;
        }

        public RestDto(int statusCode, T data, string message)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message;
        }

        public RestDto(int statusCode, T data, string? message, List<string> details)
        {
            StatusCode = statusCode;
            Data = data;
            Message = message ?? GetMessageBasedOnStatusCode(statusCode);
            Details = details;
        }
        public int StatusCode { get; private set; }
        public string? Message { get; private set; }
        public T? Data { get; private set; }
        public List<string> Details { get; set; } = new List<string>();
        private string? GetMessageBasedOnStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Success",
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate leads to career change",
                _ => null
            };
        }
    }
}
