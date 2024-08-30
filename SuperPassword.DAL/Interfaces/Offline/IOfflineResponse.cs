using SuperPassword.DAL.Implementations.Models;

namespace SuperPassword.DAL.Interfaces.Offline
{
    public interface IOfflineResponse<T> : IOfflineResponse
    {
        public T Content { get; set; }
    }

    public interface IOfflineResponse
    {
        public ResponseDataStatus? DataStatus { get; set; }
    }
}
