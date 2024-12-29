namespace Vulnerabilities.Services.EncryptionProvider
{
    public interface IEncryptionProvider
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}
