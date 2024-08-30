using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Interfaces.Offline;
using SuperPassword.DAL.Interfaces.Online;

namespace SuperPassword.BLL.Implementations.Models
{
    public class BLLResponse<T> : BLLResponseBase, IBLLResponse<T>
    {
        public IOnlineResponse<T>? OnlineResponse { get; set; }

        public IOfflineResponse<T>? OfflineResponse { get; set; }

        public T Content { get; set; }
    }

    public class BLLResponse<TIn, TOut> : BLLResponseBase, IBLLResponse<TIn, TOut>
    {
        public IOnlineResponse<TIn>? OnlineResponse { get; set; }

        public IOfflineResponse<TIn>? OfflineResponse { get; set; }

        public TOut Content { get; set; }
    }

    public class BLLResponse : BLLResponseBase, IBLLResponse
    {
        public IOnlineResponse? OnlineResponse { get; set; }

        public IOfflineResponse? OfflineResponse { get; set; }
    }

    public class BLLResponseBase : IBLLResponseBase
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }

}
