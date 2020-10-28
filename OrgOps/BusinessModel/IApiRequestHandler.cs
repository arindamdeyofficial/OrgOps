namespace Businessmodel.Common
{
    public interface IApiRequestHandler
    {
        void RaiseBusinessException(string className, string methodName, string msg);
        void LogInfo(string className, string methodName, string msg);
    }
}
