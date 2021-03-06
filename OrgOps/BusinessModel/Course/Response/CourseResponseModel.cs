﻿using System.Collections.Generic;

namespace BusinessModel.Course
{
    /// <summary>
    /// Defines the <see cref="CourseResponseModel" />.
    /// </summary>
    public class CourseResponseModel : BaseResponse
    {
        /// <summary>
        /// Courses
        /// </summary>
        public IEnumerable<CourseModel> Courses { get; set; }

    }
}
