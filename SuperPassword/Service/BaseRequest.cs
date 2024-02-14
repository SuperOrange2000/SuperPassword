using DryIoc;
using ImTools;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public void AddParameter<T>(string name, List<T> value, ParameterType type = ParameterType.GetOrPost)
        {
            foreach (var v in value)
                AddParameter(Parameter.CreateParameter(name, v, type));
        }

        public void AddParameter<T>(string name, ObservableCollection<T> value, ParameterType type = ParameterType.GetOrPost)
        {
            foreach (var v in value)
                AddParameter(Parameter.CreateParameter(name, v, type));
        }

        public void AddParameter(string name, byte[] value, ParameterType type = ParameterType.GetOrPost)
        {
            AddParameter(Parameter.CreateParameter(name, Convert.ToBase64String(value), type));
        }

        public void AddParameter(string name, object value, ParameterType type = ParameterType.GetOrPost)
        {
            AddParameter(Parameter.CreateParameter(name, value, type));
        }
    }
}
