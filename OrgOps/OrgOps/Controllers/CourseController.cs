using Businessmodel.Common;
using BusinessModel;
using BusinessModel.Course;
using Facade.Interface.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace OrgOps.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        /// <summary>
        /// For log and exception handling
        /// </summary>
        private readonly IApiRequestHandler _reqHandler;

        /// <summary>
        /// GeographyFacade inject
        /// </summary>
        private readonly ICourseFacade _courseFacade;

        public CourseController(ICourseFacade courseFacade, IApiRequestHandler reqHandler)
        {
            _reqHandler = reqHandler;
            _courseFacade = courseFacade;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CourseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("courses")]
        public async Task<ActionResult> CourseList(string courses)
        {
            var result = await _courseFacade.CourseList(
                new CourseRequestModel
                {
                    searchParams = courses.Split(",")
                }).ConfigureAwait(false);
            _reqHandler.LogInfo(MethodBase.GetCurrentMethod()?.DeclaringType?.DeclaringType?.Name
                    , MethodBase.GetCurrentMethod().Name,
                    "CourseList: Successfully Fetched the Results");
            return Ok(result);
        }
    }
}
