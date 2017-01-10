using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using OrangeTentacle.SagePay.Configuration;

namespace OrangeTentacle.SagePay.Sugar
{
    public static class Crypto
    {

        /// <summary>
        /// Encodes a string for submission to SagePay via a POST form.
        /// Uses the default provider for the decode key.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        public static string Encode(string value)
        {
            var config = ConfigurationManager.GetSection(SageConfiguration.sectionName) as SageConfiguration;

            var provider = config.Providers[config.Default];

            return Encode(provider.EncodeKey, value);
        }


        /// <summary>
        /// Encodes a string for submission to SagePay via a POST form.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public static string Encode(string key, string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Crypto key can not be null.");
            }

            if (key.Length != 16)
            {
                throw  new ArgumentOutOfRangeException(nameof(key), "Must be have length of 16");
            }

            if (value == null)
            {
                return null;
            }

            byte[] encrypted;

            using (var aes = GetAes(key))
            {                
                var encryptor = aes.CreateEncryptor();
                
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        var inputBytes = UTF8Encoding.UTF8.GetBytes(value);
                        csEncrypt.Write(inputBytes, 0, inputBytes.Length);
                    }

                    encrypted = msEncrypt.ToArray();
                }
            }

            return "@" + new SoapHexBinary(encrypted);
        }

        /// <summary>
        /// Decodes a string form response from a SagePay session.
        /// Uses the default provider for the decode key.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        
        public static string Decode(string value)
        {
            var config = ConfigurationManager.GetSection(SageConfiguration.sectionName) as SageConfiguration;

            var provider = config.Providers[config.Default];

            return Decode(provider.EncodeKey, value);
        }

        /// <summary>
        /// Decodes a string form response from a SagePay session.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public static string Decode(string key, string value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key), "Crypto key can not be null.");
            }

            if (key.Length != 16)
            {
                throw new ArgumentOutOfRangeException(nameof(key), "Must be have length of 16");
            }

            if (value == null)
            {
                return null;
            }

            if (value.StartsWith("@"))
            {
                value = value.Substring(1);
            }
            
            using (var aes = GetAes(key))
            {                
                var decrytor = aes.CreateDecryptor();
                
                using (var msEncrypt = new MemoryStream(SoapHexBinary.Parse(value).Value))
                {
                    using (var csDecrypt = new CryptoStream(msEncrypt, decrytor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static Aes GetAes(string key)
        {
            var aes = Aes.Create();

            aes.KeySize = 128;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7; // PKCS5 is 64 bit.
            aes.Key = aes.IV = UTF8Encoding.UTF8.GetBytes(key);

            return aes;
        }
    }
}
