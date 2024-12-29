namespace Vulnerabilities.Attirbutes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class EncryptColumnAttribute: Attribute
    {
    }
}
