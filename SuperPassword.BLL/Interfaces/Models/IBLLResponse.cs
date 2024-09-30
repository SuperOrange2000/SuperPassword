using SuperPassword.DAL.Interfaces.Offline;
using SuperPassword.DAL.Interfaces.Online;
using SuperPassword.DAL.Models;

namespace SuperPassword.BLL.Interfaces.Models
{
    public interface IBLLResponse<T> : IBLLResponseBase
    {
        public OnlineResponse<T>? OnlineResponse { get; }
        public OfflineResponse<T>? OfflineResponse { get; }
        public T Content { get; }
    }

    public interface IBLLResponse<TIn, TOut> : IBLLResponseBase
    {
        public OnlineResponse<TIn>? OnlineResponse { get; }
        public OfflineResponse<TIn>? OfflineResponse { get; }
        public TOut Content { get; }
    }

    public interface IBLLResponse : IBLLResponseBase
    {
        public OnlineResponse? OnlineResponse { get; }
        public OfflineResponse? OfflineResponse { get; }
    }

    public interface IBLLResponseBase
    {
        public bool IsSuccess { get; }
        public string Message { get; }
    }
}
