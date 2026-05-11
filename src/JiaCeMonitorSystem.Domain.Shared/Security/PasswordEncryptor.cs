using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace JiaCeMonitorSystem.Security
{
    /// <summary>
    /// 密码加密工具类，提供 AES 对称加密/解密功能
    /// 用于前端加密传输密码，后端解密后验证
    /// </summary>
    public static class PasswordEncryptor
    {
        /// <summary>
        /// AES 加密密码
        /// </summary>
        /// <param name="plainText">明文密码</param>
        /// <param name="key">加密密钥（建议 32 字节）</param>
        /// <returns>Base64 编码的密文</returns>
        public static string Encrypt(string plainText, string key)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;

            using var aes = Aes.Create();
            aes.Key = DeriveKey(key);
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length);

            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// AES 解密密码
        /// </summary>
        /// <param name="cipherText">Base64 编码的密文</param>
        /// <param name="key">加密密钥（建议 32 字节）</param>
        /// <returns>明文密码</returns>
        public static string Decrypt(string cipherText, string key)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;

            // 如果输入不是有效的 Base64，直接返回原字符串（兼容明文传输的过渡阶段）
            if (!IsBase64String(cipherText))
            {
                return cipherText;
            }

            try
            {
                var fullCipher = Convert.FromBase64String(cipherText);
                using var aes = Aes.Create();
                aes.Key = DeriveKey(key);

                var iv = new byte[16];
                Array.Copy(fullCipher, 0, iv, 0, 16);
                aes.IV = iv;

                using var decryptor = aes.CreateDecryptor();
                using var ms = new MemoryStream(fullCipher, 16, fullCipher.Length - 16);
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch
            {
                // 解密失败时返回原字符串（兼容明文传输的过渡阶段）
                return cipherText;
            }
        }

        /// <summary>
        /// 判断字符串是否为有效的 Base64 编码
        /// </summary>
        private static bool IsBase64String(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length % 4 != 0)
                return false;

            foreach (var c in str)
            {
                if (!char.IsLetterOrDigit(c) && c != '+' && c != '/' && c != '=')
                    return false;
            }

            try
            {
                Convert.FromBase64String(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 从字符串派生 32 字节密钥
        /// </summary>
        private static byte[] DeriveKey(string key)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        }
    }
}
