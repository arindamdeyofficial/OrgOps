using Businessmodel.Common;
using BusinessModel.Course;
using Saga.V1.Interface;
using System;
using System.Threading.Tasks;

namespace Saga.Course.V1.Implementation
{
    public class CourseSaga : IDistributedSaga<CourseRequestModel, CourseResponseModel>
    {
        /// <summary>
        /// courseAr Inject
        /// </summary>
        public readonly Lazy<ICourseAr> _courseAr;

        /// <summary>
        /// For log and exception handling
        /// </summary>
        private readonly IApiRequestHandler _reqHandler;

        public CourseSaga(IApiRequestHandler reqHandler, Lazy<ICourseAr> courseAr)
        {
            _reqHandler = reqHandler;
            _courseAr = courseAr;
        }

        public async Task<CourseResponseModel> Execute(CourseRequestModel request)
        {
            return await _courseAr.Value.CourseList(request);
        }
    }
}
