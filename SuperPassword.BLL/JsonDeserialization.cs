using SuperPassword.Entity;
using System.Text.Json;

namespace SuperPassword.BLL
{
    public class JsonDeserialization
    {
        private JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.General);
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
                ResponseBLL<T>? result;
                try
                {
                    result = JsonSerializer.Deserialize<ResponseBLL<T>>(resDAL.Content);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine(ex.Message);
                    result = new ResponseBLL<T>() { Status = System.Net.HttpStatusCode.NoContent, Message = "反序列化失败" };
                }
                finally
                {

                }
                if (result != null)
                    result.Status = resDAL.Status;
                else
                    result = new ResponseBLL<T>() { Status = System.Net.HttpStatusCode.NoContent, Message = "反序列化失败" };
                return result;
            }
        }

        public JsonDeserialization()
        {
            //options.PropertyNameCaseInsensitive = true;
        }
    }
}
