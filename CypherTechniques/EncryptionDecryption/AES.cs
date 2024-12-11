using System.Security.Cryptography;
using System.Text;

namespace CypherTechniques.EncryptionDecryption
{
    class AES
    {
        string textOriginal; 
        string key; 
        byte[] encrypted;

        public AES()
        {
            textOriginal = "";
            key = "";
        }

        public AES(string textPlain, string keyUser)
        {
            textOriginal = textPlain;
            key = keyUser;
        }

        public AES(byte[] cypher, string keyUser)
        {
            encrypted = cypher;
            key = keyUser;
        }


        public string EncryptAES()
        {
            // Create a new instance of the Aes class.  
            // This generates a new key and initialization vector (IV).
            string roundTrip;

            using (Aes myAes = Aes.Create())
            {

                // Set the aes key
                myAes.Key = Encoding.ASCII.GetBytes(key);
                // myAes.IV = Encoding.ASCII.GetBytes(key);
                // Encrypt the string to an array of bytes.
                EncryptStringToBytes_Aes(myAes.Key, myAes.IV);

                // Decrypt the bytes to a string.
                roundTrip = DecryptStringFromBytes_Aes(myAes.Key, myAes.IV);

            }
            string ciphertext = Convert.ToBase64String(encrypted);
            return 
                $"Texto cifrado Base64: {ciphertext} \n" + 
                $"Texto cifrado ASCII: {Encoding.ASCII.GetString(encrypted)} \n" + 
                $"Texto cifrado UTF8: {Encoding.UTF8.GetString(encrypted)} \n" + 
                $"Texto descifrado: {roundTrip}";
        }  

        
        public string DecryptAES()
        {
            // Create a new instance of the Aes
            // This generates a new key and initialization vector (IV).
            string roundTrip;
            using (Aes myAes = Aes.Create())
            {
                // Set the aes key
                myAes.Key = Encoding.ASCII.GetBytes(key);
                // Decrypt the bytes to a string.
                roundTrip = DecryptStringFromBytes_Aes(myAes.Key, myAes.IV);
            }
            return $"Texto descifrado: {roundTrip}";
        }     


        private void EncryptStringToBytes_Aes(byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (textOriginal == null || textOriginal.Length <= 0)
                throw new ArgumentNullException("textOriginal");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Create an Aes object with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(textOriginal);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
        }


        private string DecryptStringFromBytes_Aes(byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold the decrypted text.
            string plaintext = null;

            // Create an Aes object with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(encrypted))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}