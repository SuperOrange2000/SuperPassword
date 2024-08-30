namespace SuperPassword.BLL.Implementations.Models
{
    public enum StorageMode : byte
    {
        //CloudBackup|Server|Local
        LocalOnly = 0b001,
        ServerOnly = 0b010,
        CloudBackupOnly = 0b100,
        ServerSync = 0b011,
        CloudBackupSync = 0b101,
    }
}
