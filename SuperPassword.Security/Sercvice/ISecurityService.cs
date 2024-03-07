using SuperPassword.Security.SecurityEntityInterface;
using SuperPassword.Security.SecurityModule;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace SuperPassword.Security.Sercvice
{
    public interface ISecurityService
    {
        void SwitchCipher<T>(byte[] key) where T : ISecurityModule;
    }
}
