using SuperPassword.DAL.Implementations.Models;
using SuperPassword.DAL.Interfaces.Offline;

namespace SuperPassword.DAL.Implementations.Offline
{
    public class OfflineResponse<T> : OfflineResponse, IOfflineResponse<T>
    {
        public T Content { get; set; }
    }

    public class OfflineResponse : IOfflineResponse
    {
        public ResponseDataStatus? DataStatus { get; set; }
    }
}
