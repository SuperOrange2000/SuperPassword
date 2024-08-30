using SuperPassword.DAL.Interfaces.Offline;
using SuperPassword.DAL.Interfaces.Online;

namespace SuperPassword.BLL.Interfaces.Models
{
    public interface IBLLResponse<T> : IBLLResponseBase
    {
        public IOnlineResponse<T>? OnlineResponse { get; }
        public IOfflineResponse<T>? OfflineResponse { get; }
        public T Content { get; }
    }

    public interface IBLLResponse<TIn, TOut> : IBLLResponseBase
    {
        public IOnlineResponse<TIn>? OnlineResponse { get; }
        public IOfflineResponse<TIn>? OfflineResponse { get; }
        public TOut Content { get; }
    }

    public interface IBLLResponse : IBLLResponseBase
    {
        public IOnlineResponse? OnlineResponse { get; }
        public IOfflineResponse? OfflineResponse { get; }
    }

    public interface IBLLResponseBase
    {
        public bool IsSuccess { get; }
        public string Message { get; }
    }
}
