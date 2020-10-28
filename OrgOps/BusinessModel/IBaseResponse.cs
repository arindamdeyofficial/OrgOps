
namespace BusinessModel
{
    public interface IBaseResponse
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
    }
}
