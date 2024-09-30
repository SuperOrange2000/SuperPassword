using SuperPassword.BLL.Models;

namespace SuperPassword.BLL.Interfaces
{
    public partial interface IBLLService
    {
        Task UpdateStorageModeAsync(StorageMode mode);
    }
}
