using SuperPassword.Security.SecurityEntityInterface;
using SuperPassword.Security.SecurityModule;

namespace SuperPassword.Security.Sercvice
{
    public class SecurityService : ISecurityService
    {
        private Dictionary<Type, ISecurityModule> _securityModuleDictionary = new Dictionary<Type, ISecurityModule>();

        public T Get<T>(byte[] key) where T : ISecurityModule
        {
            if (_securityModuleDictionary.TryGetValue(typeof(T), out ISecurityModule? instance))
            {
                return (T)instance;
            }
            else
            {
                T? newSecurityModule = (T?)Activator.CreateInstance(typeof(T), key);
                if (newSecurityModule != null)
                {
                    _securityModuleDictionary.Add(typeof(T), newSecurityModule);
                    return newSecurityModule;
                }
                else
                {
                    throw new NullReferenceException();
                }
            }
        }

        public void SwitchCipher<T>(byte[] key) where T : ISecurityModule
        {
            var _cipher = Get<T>(key);
            IEncryptedBase.EncryptionHandler = _cipher.Encrypt;
            IEncryptedBase.DecryptionHandler = _cipher.Decrypt;
        }
    }
}
