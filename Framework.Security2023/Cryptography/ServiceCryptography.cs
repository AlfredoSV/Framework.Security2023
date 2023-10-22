using System;
using System.Security.Cryptography;
using System.Text;


namespace Framework.Security2023.Cryptography
{
    public class ServiceCryptography: IServiceCryptography
    {

        public string Encrypt(string str, string key)
        {
            str = str.Trim();
            key = key.Trim();

            byte[] key_byte;
            byte[] array = UTF8Encoding.UTF8.GetBytes(str); 
      
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            key_byte = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            md5.Clear();
        
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = key_byte;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convert = tripledes.CreateEncryptor(); 
            byte[] result = convert.TransformFinalBlock(array, 0, array.Length); 
            tripledes.Clear();
            return Convert.ToBase64String(result, 0, result.Length); 
        }

        public string Descrypt(string str, string key)
        {
            str = str.Trim();
            key = key.Trim();

            byte[] key_byte;
            byte[] array = Convert.FromBase64String(str); 
            
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            key_byte = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            md5.Clear();
            
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = key_byte;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convert = tripledes.CreateDecryptor();
            byte[] result = convert.TransformFinalBlock(array, 0, array.Length);
            tripledes.Clear();
            string str_des = UTF8Encoding.UTF8.GetString(result); 
            return str_des; 
        }

    }
}
