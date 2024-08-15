using Microsoft.Extensions.DependencyInjection;
using SuperPassword.Security.SecurityModule;
using SuperPassword.Security.Sercvice;

namespace SuperPassword.Security.Service
{
    public class SecurityService : ISecurityService
    {
        private ISecurityModule securityModule;
        private IServiceProvider serviceProvider;

        public SecurityService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public static void AddService(ServiceCollection container)
        {
            container.AddSingleton<AesGcmModule>();
            container.AddSingleton<ChaCha20>();
        }

        public byte[]? Decrypt(byte[] encryptedData, byte[] nonce, byte[]? tag = null)
        {
            return securityModule.Decrypt(encryptedData, nonce, tag);
        }

        public byte[]? Encrypt(byte[] plaintext, out byte[] nonce, out byte[]? tag)
        {
            return securityModule.Encrypt(plaintext, out nonce, out tag);
        }

        public void SwitchCipher(SecurityMode mode, byte[] key)
        {
            securityModule = mode switch
            {
                SecurityMode.AesGcm => serviceProvider.GetService<AesGcmModule>()!,
                SecurityMode.ChaCha20 => serviceProvider.GetService<ChaCha20>()!,
                _ => serviceProvider.GetService<AesGcmModule>()!,
            };
            securityModule.SetArguments(key);
        }
    }
}
