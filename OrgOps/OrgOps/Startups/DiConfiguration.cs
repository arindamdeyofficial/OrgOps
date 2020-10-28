using Businessmodel.Common;
using BusinessModel.Course;
using Course.V1.Implementation;
using Facade.Implementation.V1;
using Facade.Interface.V1;
using Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrgOps.Helpers;
using RepositoryLayer.Repository;
using Saga.Course.V1.Implementation;
using Saga.V1.Interface;
using System;

namespace OrgOps
{
    public static class DiConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApiRequestHandler, HandleExceptionPrivateAttribute>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserContext, UserContext>();

            //Facade Saga CQRS
            services.AddScoped<ICourseFacade, CourseFacade>();


            services.AddScoped<IDistributedSaga<CourseRequestModel, CourseResponseModel>, CourseSaga>();
            services.AddScoped(
                provider => new Lazy<IDistributedSaga<CourseRequestModel, CourseResponseModel>>
                                (provider.GetService<IDistributedSaga<CourseRequestModel, CourseResponseModel>>)
                );

            services.AddScoped<ICourseAr, CourseAr>();
            services.AddScoped(
                provider => new Lazy<ICourseAr>(provider.GetService<ICourseAr>)
                );
        }
    }
}
