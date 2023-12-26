using RestSharp;
using System.Collections.Generic;

namespace SuperPassword.Service
{
    public class BaseRequest
    {
        public Method Method { get; set; }
        public string Route { get; set; }
        public string ContentType { get; set; } = "application/x-www-form-urlencoded";
        public Dictionary<string, object> Parameters { get; set; }

        public BaseRequest()
        {
            Parameters = new Dictionary<string, object>();
        }
    }
}
