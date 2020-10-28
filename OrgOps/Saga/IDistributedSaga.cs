using BusinessModel;
using System.Threading.Tasks;

namespace Saga.V1.Interface
{
    public interface IDistributedSaga<in T, U> 
        where T: IBaseRequest where U: IBaseResponse
    {
        Task<U> Execute(T request);
    }
}
