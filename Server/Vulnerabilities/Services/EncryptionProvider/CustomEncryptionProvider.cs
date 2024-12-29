using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Vulnerabilities.Services.EncryptionProvider
{
    public class CustomEncryptionProvider : IEncryptionProvider
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public CustomEncryptionProvider(IOptions<EncryptionOptions> options)
        {
            var key = options.Value.Key;
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            _key = Encoding.UTF8.GetBytes(key);
            _iv = new byte[16];
        }

        public string Encrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(value);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(array);
        }

        public string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                var buffer = Convert.FromBase64String(value);
                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }

    public class EncryptionOptions
    {
        public string Key { get; set; }
    }
}
