using SuperPassword.Entity.Interface;

namespace SuperPassword.BLL.Interfaces.Models
{
    public interface IBLLResponse<T>
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public T? Content { get; set; }
    }

    public interface IBLLResponse
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public object? Content { get; set; }
    }
}
