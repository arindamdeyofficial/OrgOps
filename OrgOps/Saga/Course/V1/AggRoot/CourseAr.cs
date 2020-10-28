using Businessmodel.Common;
using BusinessModel;
using BusinessModel.Course;
using DbModels;
using RepositoryLayer.Repository;
using Saga.V1.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Course.V1.Implementation
{
    public class CourseAr: ICourseAr
    {
        /// <summary>
        /// For log and exception handling
        /// </summary>
        private readonly IApiRequestHandler _reqHandler;

        /// <summary>
        /// Gets the UnitOfWork.
        /// </summary>
        private IUnitOfWork _unitOfWork { get; }

        /// <summary>
        /// Gets or sets the courseRepository.
        /// </summary>
        private IDbRepository<CourseDbEntity> _courseRepository { get; set; }

        public CourseAr(IApiRequestHandler reqHandler, IUnitOfWork unitOfWork)
        {
            _reqHandler = reqHandler;
            _courseRepository = unitOfWork.GetRepository<CourseDbEntity>();
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The CourseList
        /// </summary>
        /// <returns>The <see cref="CourseEntity"/> CourseEntity List.</returns>
        public async Task<CourseResponseModel> CourseList(CourseRequestModel request)
        {
            return new CourseResponseModel
            {
                IsSuccess = true,
                Courses = (await _courseRepository.GetFirstOrDefaultAsyncGroupBy(x =>
                    new CourseDbEntity
                    {
                        CourseId = x.CourseId,
                        CourseName = x.CourseName,
                        CourseDesc = x.CourseDesc
                    }
                )).Select(x => new CourseModel
                {
                    CourseId = x.CourseId,
                    CourseName = x.CourseName,
                    CourseDesc = x.CourseDesc
                })
            };
        }
    }
}
