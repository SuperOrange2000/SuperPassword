using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using SuperPassword.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperPassword.BLL
{
    public class JsonDeserialization
    {
        internal ResponseBLL<T> Deserialize<T>(ResponseDAL resDAL)
        {
            if (string.IsNullOrEmpty(resDAL.Content))
            {
                ResponseBLL<T> result = new ResponseBLL<T>();
                result.Status = resDAL.Status;
                return result;
            }
            else
            {
                ResponseBLL<T>? result = JsonConvert.DeserializeObject<ResponseBLL<T>>(resDAL.Content);
                if (result != null)
                    result.Status = resDAL.Status;
                else
                    result = new ResponseBLL<T>() { Status = System.Net.HttpStatusCode.NoContent, Message = "反序列化失败" };
                return result;
            }
        }

        internal ResponseBLL<T> Deserialize<T>(ResponseDAL resDAL, JsonSerializerSettings serializerSettings)
        {
            if (string.IsNullOrEmpty(resDAL.Content))
            {
                ResponseBLL<T> result = new ResponseBLL<T>();
                result.Status = resDAL.Status;
                return result;
            }
            else
            {
                ResponseBLL<T>? result = JsonConvert.DeserializeObject<ResponseBLL<T>>(resDAL.Content, serializerSettings);
                if (result != null)
                    result.Status = resDAL.Status;
                else
                    result = new ResponseBLL<T>() { Status = System.Net.HttpStatusCode.NoContent, Message = "反序列化失败" };
                return result;
            }
        }
    }
}
