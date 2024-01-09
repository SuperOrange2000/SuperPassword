using RestSharp;
using System;
using System.Collections.Generic;

namespace SuperPassword.Service
{
    public class BaseRequest : RestRequest
    {
        public Method Method { get; set; }
        public string Route { get; set; }
        public string ContentType { get; set; } = "application/x-www-form-urlencoded";
        public Dictionary<string, object> Parameters { get; set; }

        public BaseRequest() : base()
        {
            Parameters = new Dictionary<string, object>();
        }

        public BaseRequest(string route, Method method = Method.Get) : base(new Uri(@"https://s.oragne.top/" + route), method)
        {
            Parameters = new Dictionary<string, object>();
        }
    }
}
