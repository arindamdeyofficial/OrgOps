using AutoMapper;
using Businessmodel.Common;
using BusinessModel;
using BusinessModel.Course;
using DbModels;
using RepositoryLayer.Repository;
using Saga.V1.Interface;
using System.Collections.Generic;
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

        /// <summary>
        /// Gets the Mapper.
        /// </summary>
        private IMapper _mapper { get; }

        public CourseAr(IApiRequestHandler reqHandler, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _reqHandler = reqHandler;
            _courseRepository = unitOfWork.GetRepository<CourseDbEntity>();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
                Courses = _mapper.Map<IEnumerable<CourseDbEntity>, IEnumerable<CourseModel>>(
                    await _courseRepository.GetAsync(x => request.searchParams.Contains(x.CourseId.ToString()), null, m=>m.CourseId))
            };
        }
    }
}
