// *****************************************************
// * Copyright 2017, SportingApp, all rights reserved. *
// * Author: Shih Peiting                              *
// * mailto: sportingapp@gmail.com                     *
// *****************************************************
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace SaGUtil.Security
{
    public static class SaCryptography
    {
        /// <summary>
        ///         ''' File MD5
        ///         ''' </summary>
        ///         ''' <param name="file_full_path"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public static byte[] FileMD5(string file_full_path)
        {
            if (File.Exists(file_full_path))
            {
                using (var md = MD5.Create())
                {
                    using (var stream = File.OpenRead(file_full_path))
                    {
                        return md.ComputeHash(stream);
                    }
                }
            }

            throw new Exception(file_full_path + " is not exists");
        }

        public static byte[] TextMD5(string s)
        {
            using (var md = MD5.Create())
            {
                return md.ComputeHash(Encoding.UTF8.GetBytes(s));
            }
        }

        public static byte[] TextSHA256(string s)
        {
            using (SHA256 sha = new SHA256CryptoServiceProvider())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(s));
            }
        }

        /// <summary>
        ///         ''' 加密字串 (公鑰加密)
        ///         ''' </summary>
        ///         ''' <param name="publickey"></param>
        ///         ''' <param name="content"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public static string RSAEncrypt(string publickey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publickey);

            string encryptString = Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(content), false));

            return encryptString;
        }

        /// <summary>
        ///         ''' 解密字串 (私鑰解密)
        ///         ''' </summary>
        ///         ''' <param name="privatekey"></param>
        ///         ''' <param name="content"></param>
        ///         ''' <remarks></remarks>
        public static string RSADecrypt(string privatekey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            try
            {
                if (string.IsNullOrEmpty(content))
                    return string.Empty;
                rsa.FromXmlString(privatekey);

                var decryptString = Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(content), false));

                return decryptString;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return string.Empty;
            }
        }

        /// <summary>
        ///         ''' SHA1 產生簽章資料
        ///         ''' </summary>
        ///         ''' <param name="privatekey"></param>
        ///         ''' <param name="dataToSign"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public static byte[] RSAhashAnsSign(string privatekey, byte[] dataToSign)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privatekey);
            return rsa.SignData(dataToSign, new SHA1CryptoServiceProvider());
        }

        /// <summary>
        ///         ''' 驗證簽章資料
        ///         ''' </summary>
        ///         ''' <param name="data"></param>
        ///         ''' <param name="signature"></param>
        ///         ''' <param name="publicKey"></param>
        ///         ''' <returns></returns>
        ///         ''' <remarks></remarks>
        public static bool VerifySignedData(byte[] data, byte[] signature, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey); // 需要用到傳送者的公鑰將簽章解密

            return rsa.VerifyData(data, new SHA1CryptoServiceProvider(), signature);
        }

        /// <summary>
        ///         ''' 對稱式加密
        ///         ''' </summary>
        ///         ''' <param name="plainText"></param>
        ///         ''' <param name="key"></param>
        ///         ''' <returns></returns>
        public static string TextEncrypt(string plainText, string key)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] plainTextData = Encoding.Unicode.GetBytes(plainText);
            byte[] keyData = MD5.ComputeHash(Encoding.Unicode.GetBytes(key));
            byte[] IVData = MD5.ComputeHash(Encoding.Unicode.GetBytes("Super SportingApp For SaGUtil"));
            AES.Key = keyData;
            AES.IV = IVData;
            AES.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = AES.CreateEncryptor();
            // Dim transform As ICryptoTransform = AES.CreateEncryptor(keyData, IVData)
            byte[] outputData = transform.TransformFinalBlock(plainTextData, 0, plainTextData.Length);
            return Convert.ToBase64String(outputData);
        }

        /// <summary>
        ///         ''' 對稱式解密
        ///         ''' </summary>
        ///         ''' <param name="cipherTextData"></param>
        ///         ''' <param name="key"></param>
        ///         ''' <returns></returns>
        public static string TextDecrypt(byte[] cipherTextData, string key)
        {
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] keyData = MD5.ComputeHash(Encoding.Unicode.GetBytes(key));
            byte[] IVData = MD5.ComputeHash(Encoding.Unicode.GetBytes("Super SportingApp For SaGUtil"));
            AES.Key = keyData;
            AES.IV = IVData;
            AES.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = AES.CreateDecryptor();
            // Dim transform As ICryptoTransform = AES.CreateDecryptor(keyData, IVData)
            byte[] outputData = transform.TransformFinalBlock(cipherTextData, 0, cipherTextData.Length);
            return Encoding.Unicode.GetString(outputData);
        }

        public static string TextDecrypt(string encryptData, string key)
        {
            byte[] cipherTextData = Convert.FromBase64String(encryptData);
            RijndaelManaged AES = new RijndaelManaged();
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] keyData = MD5.ComputeHash(Encoding.Unicode.GetBytes(key));
            byte[] IVData = MD5.ComputeHash(Encoding.Unicode.GetBytes("Super SportingApp For SaGUtil"));
            AES.Key = keyData;
            AES.IV = IVData;
            AES.Padding = PaddingMode.PKCS7;
            ICryptoTransform transform = AES.CreateDecryptor();
            // Dim transform As ICryptoTransform = AES.CreateDecryptor(keyData, IVData)
            byte[] outputData = transform.TransformFinalBlock(cipherTextData, 0, cipherTextData.Length);
            return Encoding.Unicode.GetString(outputData);
        }
    }
}
