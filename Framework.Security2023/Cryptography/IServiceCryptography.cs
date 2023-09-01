
namespace Framework.Security2023.Cryptography
{
    public interface IServiceCryptography
    {
        string Encrypt(string str, string key);
        string Descrypt(string str, string key);
    }
}
