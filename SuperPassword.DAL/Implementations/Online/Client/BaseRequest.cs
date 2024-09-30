using RestSharp;
using System.Text.Json;
using System.Xml.Linq;

namespace SuperPassword.DAL.Online.Client
{
    public class BaseRequest : RestRequest
    {
        private static Uri baseUri;

        public static void SetBaseUri(string uri)
        {
            baseUri ??= new Uri(uri);
        }

        private static string? token;

        public static void SetToken(string token)
        {
            BaseRequest.token = token;
        }

        private Dictionary<string, string> jsonData = [];

        public BaseRequest() : base() { }

        public BaseRequest(string route, Method method = Method.Get) : base(new Uri(baseUri, route), method)
        {
            if (method == Method.Post)
                AddParameter(new HeaderParameter("Content-Type", "application/json"));
            else
                AddParameter(new HeaderParameter("Content-Type", "application/x-www-form-urlencoded"));

            if (token != null)
            {
                AddParameter(new HeaderParameter("Authorization", token));
            }
        }
    }
}
