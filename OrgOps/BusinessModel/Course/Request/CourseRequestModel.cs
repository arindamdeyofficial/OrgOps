
using System.Collections.Generic;

namespace BusinessModel.Course
{
    /// <summary>
    /// Defines the <see cref="CourseRequestModel" />.
    /// </summary>
    public class CourseRequestModel : BaseRequest
    {
        public IEnumerable<string> searchParams { get; set; }
    }
}
