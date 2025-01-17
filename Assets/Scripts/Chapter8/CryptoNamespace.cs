namespace CryptoNamespace
{
    using System;
    using System.Text;
    using System.Security.Cryptography;

    /// <summary>
    /// This class provides cryptographic methods for AES encryption/decryption
    /// and a simple XOR-based encryption.
    /// </summary>
    public class CryptoMethods
    {
        // AES Key: Must be 32 characters (256-bit key) for AES encryption
        private const string aesKey = "b28a74327a1b20b4311a3976acf096cd";

        // AES Initialization Vector (IV): Must be 16 characters (128-bit block size)
        private const string aesIv = "unity4profession";

        /// <summary>
        /// Encrypts a plaintext string using AES encryption.
        /// </summary>
        /// <param name="inputData">The plaintext string to encrypt.</param>
        /// <returns>The encrypted string encoded in Base64 format.</returns>
        public static string AESEncrypt(string inputData)
        {
            // Use a disposable AES crypto provider to manage encryption
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.BlockSize = 128; // The block size for AES (128 bits)
                aes.KeySize = 256; // The key size for AES (256 bits)
                aes.Key = Encoding.ASCII.GetBytes(aesKey); // Convert key to byte array
                aes.IV = Encoding.ASCII.GetBytes(aesIv); // Convert IV to byte array
                aes.Mode = CipherMode.CBC; // Use Cipher Block Chaining (CBC) mode
                aes.Padding = PaddingMode.PKCS7; // Use PKCS7 padding to handle block size alignment

                // Convert the input string into a byte array
                byte[] inputBytes = Encoding.ASCII.GetBytes(inputData);

                // Create an encryptor using the specified key and IV
                ICryptoTransform encryptor = aes.CreateEncryptor();

                // Perform the encryption and get the encrypted byte array
                byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                // Convert the encrypted bytes into a Base64 string and return it
                return Convert.ToBase64String(encryptedBytes);
            }
        }

        /// <summary>
        /// Decrypts a Base64-encoded string using AES decryption.
        /// </summary>
        /// <param name="inputData">The Base64-encoded string to decrypt.</param>
        /// <returns>The decrypted plaintext string.</returns>
        public static string AESDecrypt(string inputData)
        {
            // Use a disposable AES crypto provider to manage decryption
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.BlockSize = 128; // AES block size (128 bits)
                aes.KeySize = 256; // AES key size (256 bits)
                aes.Key = Encoding.ASCII.GetBytes(aesKey); // Convert key to byte array
                aes.IV = Encoding.ASCII.GetBytes(aesIv); // Convert IV to byte array
                aes.Mode = CipherMode.CBC; // Use Cipher Block Chaining (CBC) mode
                aes.Padding = PaddingMode.PKCS7; // Use PKCS7 padding

                // Convert the Base64-encoded string into a byte array
                byte[] encryptedBytes = Convert.FromBase64String(inputData);

                // Create a decryptor using the specified key and IV
                ICryptoTransform decryptor = aes.CreateDecryptor();

                // Perform the decryption and get the decrypted byte array
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                // Convert the decrypted bytes into a string and return it
                return Encoding.ASCII.GetString(decryptedBytes);
            }
        }

        /// <summary>
        /// Encrypts a plaintext string using a simple XOR cipher and encodes the result in Base64.
        /// </summary>
        /// <param name="input">The string to encrypt.</param>
        /// <param name="key">The key used for XOR encryption.</param>
        /// <returns>The encrypted string encoded in Base64 format.</returns>
        public static string XorEncrypt(string input, string key)
        {
            // Create a character array to store the XOR result
            char[] output = new char[input.Length];

            // XOR each character of the input string with the corresponding character from the key
            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (char)(input[i] ^ key[i % key.Length]);
            }

            // Convert the result to a Base64-encoded string and return it
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(new string(output)));
        }

        /// <summary>
        /// Decrypts a Base64-encoded string that was encrypted using XOR cipher.
        /// </summary>
        /// <param name="encrypted">The Base64-encoded string to decrypt.</param>
        /// <param name="key">The key used for XOR encryption.</param>
        /// <returns>The decrypted plaintext string.</returns>
        public static string XorDecrypt(string encrypted, string key)
        {
            // Decode the Base64-encoded string back into its original XOR-encrypted form
            string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encrypted));

            // Create a character array to store the XOR result
            char[] output = new char[decoded.Length];

            // XOR each character of the decoded string with the corresponding character from the key
            for (int i = 0; i < decoded.Length; i++)
            {
                output[i] = (char)(decoded[i] ^ key[i % key.Length]);
            }

            // Convert the result back to a plaintext string and return it
            return new string(output);
        }
    }
}