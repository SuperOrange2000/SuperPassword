using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class SecurityModule
{
    private static byte[] key; // 16字节密钥  
    private static byte[] iv; // 16字节初始向量  

    public SecurityModule()
    {
        key = Encoding.UTF8.GetBytes("1234567890123456");
        iv = Encoding.UTF8.GetBytes("1234567890123456");
    }

    public static string Encrypt(string plainText)
    {
        try
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("加密失败：" + ex.Message);
        }
    }

    public static string Decrypt(string cipherText)
    {
        try
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("解密失败：" + ex.Message);
        }
    }
}