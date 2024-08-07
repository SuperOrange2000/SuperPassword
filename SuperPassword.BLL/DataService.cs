using SuperPassword.DAL;
using SuperPassword.DAL.Models;
using SuperPassword.Entity.Interface;
using SuperPassword.BLL.Models;


namespace SuperPassword.BLL
{
    public class DataService : JsonDeserialization, IDataServiceBLL
    {
        private IDataServiceDAL _dataServiceDAL { get; init; }

        public DataService(IDataServiceDAL dataDAL)
        {
            _dataServiceDAL = dataDAL;
        }

        public async Task<ResponseBLL<object>> AddAsync(string username, string token, IInfoGroup infoGroup)
        {
            ResponseDAL responseDAL = await _dataServiceDAL.AddAsync(username, token, infoGroup);
            return Deserialize<object>(responseDAL);
        }

        public async Task<ResponseBLL<object>> DeleteAsync(string username, string token, uint id)
        {
            ResponseDAL responseDAL = await _dataServiceDAL.DeleteAsync(username, token, id);
            return Deserialize<object>(responseDAL);
        }

        public async Task<ResponseBLL<List<IInfoGroup>>> GetAllAsync(string username, string token)
        {
            ResponseDAL responseDAL = await _dataServiceDAL.GetAllAsync(username, token);
            return Deserialize<List<IInfoGroup>>(responseDAL);
        }

        public async Task<ResponseBLL<IInfoGroup>> GetFirstOfDefaultAsync(string username, string token, uint id)
        {
            ResponseDAL responseDAL = await _dataServiceDAL.GetFirstOfDefaultAsync(username, token, id);
            return Deserialize<IInfoGroup>(responseDAL);
        }

        public async Task<ResponseBLL<IInfoGroup>> UpdateAsync(string username, string token, IInfoGroup infoGroup)
        {
            ResponseDAL responseDAL = await _dataServiceDAL.UpdateAsync(username, token, infoGroup);
            return Deserialize<IInfoGroup>(responseDAL);
        }
    }
}
