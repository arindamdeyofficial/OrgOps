
namespace BusinessModel
{
    public class BaseResponse: IBaseResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
