using SuperPassword.DAL.Interfaces.Offline;

namespace SuperPassword.DAL.Models
{
    public class OfflineResponse<T> : OfflineResponse
    {
        public T Content { get; set; }
    }

    public class OfflineResponse
    {
        public ResponseDataStatus? DataStatus { get; set; }
    }
}
