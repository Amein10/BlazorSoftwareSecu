using System.Security.Cryptography;
using System.Text;

namespace BlazorSoftwareSecu.Services
{
    public class CprEncryptionService
    {
        private readonly IConfiguration _configuration;

        public CprEncryptionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Encrypt(string plainText)
        {
            var key = GetKey();

            using var aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();

            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            var result = new byte[aes.IV.Length + encryptedBytes.Length];

            Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
            Buffer.BlockCopy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);

            return Convert.ToBase64String(result);
        }

        public string Decrypt(string cipherText)
        {
            var key = GetKey();

            var fullCipher = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = key;

            var iv = new byte[aes.BlockSize / 8];
            var cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, cipher.Length);

            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();

            var decryptedBytes = decryptor.TransformFinalBlock(cipher, 0, cipher.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        private byte[] GetKey()
        {
            var keyText = _configuration["CprEncryptionKey"];

            if (string.IsNullOrWhiteSpace(keyText))
            {
                throw new InvalidOperationException("CprEncryptionKey mangler i appsettings.json");
            }

            return SHA256.HashData(Encoding.UTF8.GetBytes(keyText));
        }
    }
}