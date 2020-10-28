using Microsoft.AspNetCore.Http;

namespace OrgOps.Helpers
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CurrentUser()
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;
            return username;
        }
    }
}
