using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace DotNetLib.utils
{
    public class DES
    {

        public static Byte[] key = { 12, 23, 34, 45, 56, 67, 78, 89 };
        public static Byte[] iv = { 120, 230, 10, 1, 10, 20, 30, 40 };

        public static string Crypto(String str)
        {
            return MyDESCrypto(str, key, iv);
        }

        public static string MyDESCrypto(string str, byte[] keys, byte[] ivs)
        {
            //加密
            byte[] strs = Encoding.Unicode.GetBytes(str);

            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            ICryptoTransform transform = desc.CreateEncryptor(keys, ivs);//加密对象
            CryptoStream cStream = new CryptoStream(mStream, transform, CryptoStreamMode.Write);
            cStream.Write(strs, 0, strs.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        public static string CryptoDe(String str)
        {
            return MyDESCryptoDe(str, key, iv);
        }
        public static string MyDESCryptoDe(string str, byte[] keys, byte[] ivs)
        {
            //解密
            byte[] strs = Convert.FromBase64String(str);
            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            ICryptoTransform transform = desc.CreateDecryptor(keys, ivs);//解密对象
            CryptoStream cStream = new CryptoStream(mStream, transform, CryptoStreamMode.Write);
            cStream.Write(strs, 0, strs.Length);
            cStream.FlushFinalBlock();
            return Encoding.Unicode.GetString(mStream.ToArray());
        }
    }
}
