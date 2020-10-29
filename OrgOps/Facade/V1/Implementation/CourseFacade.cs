using BusinessModel;
using BusinessModel.Course;
using Facade.Interface.V1;
using Saga.V1.Interface;
using System;
using System.Threading.Tasks;

namespace Facade.Implementation.V1
{
    public class CourseFacade : ICourseFacade
    {
        /// <summary>
        /// PloyPolicyHandler
        /// </summary>
        private readonly IPolyDbPolicyHandler _ployPolicyHandler;

        /// <summary>
        /// GeoSaga
        /// </summary>
        private readonly Lazy<IDistributedSaga<CourseRequestModel, CourseResponseModel>> _courseSaga;

        public CourseFacade(
            Lazy<IDistributedSaga<CourseRequestModel, CourseResponseModel>>
                courseSaga,
            IPolyDbPolicyHandler ployPolicyHandler)
        {
            _ployPolicyHandler = ployPolicyHandler;
            _courseSaga = courseSaga;
        }
        public async Task<CourseResponseModel> CourseList(CourseRequestModel request)
        {
            CourseResponseModel a = new CourseResponseModel();
            try
            {
                a = await _ployPolicyHandler.GetPollyPolicyConfiguration()
                   .ExecuteAsync(async () => await _courseSaga.Value.Execute(request).ConfigureAwait(false))
                   .ConfigureAwait(false);
            }
            catch(Exception ex)
            {

            }
            return a;
        }
    }
}
