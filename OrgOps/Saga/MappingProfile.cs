using AutoMapper;
using BusinessModel;
using DbModels;

namespace Saga.Mapper
{
    /// <summary>
    /// Defines the <see cref="MappingProfile" />.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<CourseModel, CourseDbEntity>();
        }

    }
}
