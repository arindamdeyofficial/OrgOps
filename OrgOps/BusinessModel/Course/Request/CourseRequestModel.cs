
using System.Collections.Generic;

namespace BusinessModel.Geography
{
    /// <summary>
    /// Defines the <see cref="CourseRequestModel" />.
    /// </summary>
    public class CourseRequestModel : BaseRequest
    {
        public IEnumerable<string> searchParams { get; set; }
    }
}
