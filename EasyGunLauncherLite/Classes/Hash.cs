using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using RSAxPlus.ArpanTECH;

namespace EasyGunLauncherLite
{
    public class Hash
    {
        private static string _privateKey;
        private static string _publicKey;
        private static UnicodeEncoding _encoder = new UnicodeEncoding();

        private static string filenamePrivateKeyXml = "k0G6OFGg3DH5IYv2";
        private static string filenamePublicKeyXml = "6HeRa7wVNp2e0Eu2";

        public static void LoadKeys()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNamePublicKeyXml = "EasyGunLauncherLite.Hash." + filenamePublicKeyXml;
            var resourceNamePrivateKeyXml = "EasyGunLauncherLite.Hash." + filenamePrivateKeyXml;

            using (Stream stream = assembly.GetManifestResourceStream(resourceNamePublicKeyXml))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    // _publicKey = reader.ReadToEnd();
                    string keyLoad = reader.ReadToEnd();
                    byte[] keyBytes = Base64Decoding(keyLoad);
                    _publicKey = Encoding.UTF8.GetString(keyBytes, 0, keyBytes.Length);
                }
            }
            
            using (Stream stream = assembly.GetManifestResourceStream(resourceNamePrivateKeyXml))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    // _privateKey = reader.ReadToEnd();
                    string keyLoad = reader.ReadToEnd();
                    byte[] keyBytes = Base64Decoding(keyLoad);
                    _privateKey = Encoding.UTF8.GetString(keyBytes, 0, keyBytes.Length);
                }
            }
            
        }

        /**
         * return base64 of encrypted string
         */
        public static string Encrypt(string data)
        {
            // RSACryptoServiceProvider RSAalgPl = new RSACryptoServiceProvider(2048);
            // RSAalgPl.FromXmlString(_publicKey);
            // RSACryptoServiceProvider RSAalgPr = new RSACryptoServiceProvider(2048);
            // RSAalgPr.FromXmlString(_privateKey);
            RSAx rsax = RSAx.CreateFromPEM(_publicKey);
            byte[] ctx = rsax.Encrypt(Encoding.UTF8.GetBytes(data), false);
            return Convert.ToBase64String(ctx);
            // byte[] dataToEncrypt = Encoding.UTF8.GetBytes(data);
            // byte[] encrypted = rsaEncryptionOaepSha1(_publicKey, dataToEncrypt);
            // return Base64Encoding(encrypted);
        }

        /**
         * return plaintext of base64 encrypted string (encrypt with public key)
         */
        public static string Decrypt(string data)
        {
            byte[] ciphertextReceived = Base64Decoding(data);
            byte[] decryptedtextByte = rsaDecryptionOaepSha1(_privateKey, ciphertextReceived);
            return Encoding.UTF8.GetString(decryptedtextByte, 0, decryptedtextByte.Length);
        }

        public static byte[] rsaEncryptionOaepSha1(string publicKeyXml, byte[] plaintext) {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(2048);
            RSAalg.PersistKeyInCsp = false;
            RSAalg.FromXmlString(publicKeyXml);
            return RSAalg.Encrypt(plaintext, false);
        }

        public static byte[] rsaDecryptionOaepSha1(string privateKeyXml, byte[] ciphertext) {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(2048);
            RSAalg.PersistKeyInCsp = false;
            RSAalg.FromXmlString(privateKeyXml);
            return RSAalg.Decrypt(ciphertext, false);
        }
        
        static string Base64Encoding(byte[] input) {
            return Convert.ToBase64String(input);
        }

        static byte[] Base64Decoding(String input) {
            return Convert.FromBase64String(input);
        }
    }
}