using BusinessModel.Course;
using System.Threading.Tasks;

namespace Saga.V1.Interface
{
    public interface ICourseAr
    {
        Task<CourseResponseModel> CourseList(CourseRequestModel request);
    }
}
