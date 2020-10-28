using BusinessModel.Course;
using System.Threading.Tasks;

namespace Facade.Interface.V1
{
    public interface ICourseFacade
    {
        Task<CourseResponseModel> CourseList(CourseRequestModel request);
    }
}
