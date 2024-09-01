using SuperPassword.BLL.Implementations.Models;

namespace SuperPassword.BLL.Interfaces
{
    public partial interface IBLLService
    {
        Task UpdateStorageModeAsync(StorageMode mode);
    }
}
