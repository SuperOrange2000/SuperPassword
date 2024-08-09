using SuperPassword.BLL.Implementations.Models;
using SuperPassword.DAL.Interfaces.Models;
using SuperPassword.Entity.Interface;
using System.Text.Json;

namespace SuperPassword.BLL.Implementations
{
    public class JsonDeserialization
    {
        private JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.General);
        internal BLLResponse<T> Deserialize<T>(IDALResponse resDAL)
        {
            if (string.IsNullOrEmpty(resDAL.Content))
            {
                BLLResponse<T> result = new BLLResponse<T>();
                //result.Status = resDAL.Status;
                return result;
            }
            else
            {
                BLLResponse<T>? result;
                try
                {
                    result = JsonSerializer.Deserialize<BLLResponse<T>>(resDAL.Content);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine(ex.Message);
                    result = new BLLResponse<T>() { Status = ResponseStatus.NoContent, Message = "反序列化失败" };
                }
                finally
                {

                }
                if (result != null)
                    result.Status = resDAL.Status;
                else
                    result = new BLLResponse<T>() { Status = ResponseStatus.NoContent, Message = "反序列化失败" };
                return result;
            }
        }

        public JsonDeserialization()
        {
            //options.PropertyNameCaseInsensitive = true;
        }
    }
}
