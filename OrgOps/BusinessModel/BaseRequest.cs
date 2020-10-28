
using System;

namespace BusinessModel
{
    public class BaseRequest : IBaseRequest
    {
        public Guid RequestId { get; set; }
        public string Userid { get; set; }
    }
}
