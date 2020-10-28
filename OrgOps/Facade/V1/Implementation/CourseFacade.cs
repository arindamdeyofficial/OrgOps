using Facade;
using Facade.Interface.V1;
using System;
using System.Threading.Tasks;

namespace SGRE.SiteEnrichment.Facade.Implementation.V1
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
        //private readonly Lazy<IDistributedSaga<GeographyRequestModel, GeographyResponseModel>> _courseSaga;

        //public CourseFacade(
        //    Lazy<IDistributedSaga<CoordinateReferenceSystemRequestModel, CoordinateReferenceSystemResponseModel>>
        //        courseSaga,
        //    IPolyDbPolicyHandler ployPolicyHandler)
        //{
        //    _ployPolicyHandler = ployPolicyHandler;
        //    _courseSaga = courseSaga;
        //}
        //public async Task<CourseModel> CourseList()
        //{
        //    return await _ployPolicyHandler.GetPollyPolicyConfiguration()
        //       .ExecuteAsync(async () => await _courseSaga.Value.Execute(null))
        //       .ConfigureAwait(false);
        //}
    }
}
