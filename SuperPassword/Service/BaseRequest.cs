using DryIoc;
using ImTools;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SuperPassword.Service
{
    public class BaseRequest : RestRequest
    {

        public BaseRequest() : base() { }

        public BaseRequest(string route, Method method = Method.Get) : base(new Uri(@"https://s.oragne.top/" + route), method)
        {
            AddParameter(new HeaderParameter("Content-Type", "application/x-www-form-urlencoded"));
        }

        public void AddParameter(string name, object value, ParameterType type = ParameterType.GetOrPost)
        {
            if (value is List<string>)
                foreach (var kvp2 in (List<string>)value)
                    AddParameter(Parameter.CreateParameter(name, kvp2, type));
            else
                AddParameter(Parameter.CreateParameter(name, value, type));
        }
    }
}
