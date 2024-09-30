using SuperPassword.BLL.Interfaces.Models;
using SuperPassword.DAL.Models;

namespace SuperPassword.BLL.Implementations.Models
{
    public class BLLResponse<T> : BLLResponseBase, IBLLResponse<T>
    {
        public OnlineResponse<T>? OnlineResponse { get; set; }

        public OfflineResponse<T>? OfflineResponse { get; set; }

        public T Content { get; set; }
    }

    public class BLLResponse<TIn, TOut> : BLLResponseBase, IBLLResponse<TIn, TOut>
    {
        public OnlineResponse<TIn>? OnlineResponse { get; set; }

        public OfflineResponse<TIn>? OfflineResponse { get; set; }

        public TOut Content { get; set; }
    }

    public class BLLResponse : BLLResponseBase, IBLLResponse
    {
        public OnlineResponse? OnlineResponse { get; set; }

        public OfflineResponse? OfflineResponse { get; set; }
    }

    public class BLLResponseBase : IBLLResponseBase
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }

}
