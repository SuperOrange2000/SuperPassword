using RestSharp;

namespace SuperPassword.DAL.Online.Client
{
    public class BaseRequest : RestRequest
    {

        public BaseRequest() : base() { }

        public BaseRequest(string route, Method method = Method.Get) : base(new Uri(@"https://s.oragne.top/" + route), method)
        {
            AddParameter(new HeaderParameter("Content-Type", "application/x-www-form-urlencoded"));
        }

        public void AddParameter<T>(string? name, IList<T> value, ParameterType type = ParameterType.GetOrPost)
        {
            foreach (var v in value)
                AddParameter(name, v, type);
        }

        public void AddParameter<T, TResult>(string? name, IList<T> value, Func<T, TResult> implementationFactory, ParameterType type = ParameterType.GetOrPost)
        {
            foreach (var v in value)
                AddParameter(name, implementationFactory.Invoke(v), type);
        }

        public void AddParameter<T>(string? name, ICollection<T> value, ParameterType type = ParameterType.GetOrPost)
        {
            foreach (var v in value)
                AddParameter(name, v, type);
        }

        public void AddParameter<T, TResult>(string? name, ICollection<T> value, Func<T, TResult> implementationFactory, ParameterType type = ParameterType.GetOrPost)
        {
            foreach (var v in value)
                AddParameter(name, implementationFactory.Invoke(v), type);
        }

        public void AddParameter<T>(string? name, T value, ParameterType type = ParameterType.GetOrPost)
            => AddParameter(Parameter.CreateParameter(name, value, type));

        public void AddParameter(string? name, byte[] value, ParameterType type = ParameterType.GetOrPost) =>
            AddParameter(Parameter.CreateParameter(name, Convert.ToBase64String(value), type));

        public void AddParameter(string? name, object? value, ParameterType type = ParameterType.GetOrPost) =>
            AddParameter(Parameter.CreateParameter(name, value, type));
    }
}
