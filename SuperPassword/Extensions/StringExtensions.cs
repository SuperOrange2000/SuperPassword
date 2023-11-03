﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace SuperPassword.Extensions
{
    public static class StringExtensions
    {
        public static string GetMD5(this string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException(nameof(data));

            var hash = MD5.Create().ComputeHash(Encoding.Default.GetBytes(data));
            return Convert.ToBase64String(hash);//将加密后的字节数组转换为加密字符串
        }
    }
}
