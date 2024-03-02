using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using SuperPassword.DAL;
using SuperPassword.Entity;
using SuperPassword.Entity.Data;


namespace SuperPassword.BLL
{
    public class DataService : JsonDeserialization, IDataServiceBLL
    {
        private IDataServiceDAL _dataServiceDAL { get; init; }

        public DataService(IDataServiceDAL dataDAL)
        {
            _dataServiceDAL = dataDAL;
        }

        public async Task<ResponseBLL<object>> AddAsync(UserEntity user, InfoGroupEntity infoGroup)
        {
            ResponseDAL responseDAL = await _dataServiceDAL.AddAsync(user, infoGroup);
            return Deserialize<object>(responseDAL);
        }

        public async Task<ResponseBLL<object>> DeleteAsync(UserEntity user, uint id)
        {
            ResponseDAL responseDAL = await _dataServiceDAL.DeleteAsync(user, id);
            return Deserialize<object>(responseDAL);
        }

        public async Task<ResponseBLL<List<InfoGroupEntity>>> GetAllAsync(UserEntity user)
        {
            ResponseDAL responseDAL = await _dataServiceDAL.GetAllAsync(user);
            return Deserialize<List<InfoGroupEntity>>(responseDAL);
        }

        public async Task<ResponseBLL<InfoGroupEntity>> GetFirstOfDefaultAsync(UserEntity user, uint id)
        {
            ResponseDAL responseDAL = await _dataServiceDAL.GetFirstOfDefaultAsync(user, id);
            return Deserialize<InfoGroupEntity>(responseDAL);
        }

        public async Task<ResponseBLL<InfoGroupEntity>> UpdateAsync(UserEntity user, InfoGroupEntity infoGroup)
        {
            ResponseDAL responseDAL = await _dataServiceDAL.AddAsync(user, infoGroup);
            return Deserialize<InfoGroupEntity>(responseDAL);
        }
    }
}
