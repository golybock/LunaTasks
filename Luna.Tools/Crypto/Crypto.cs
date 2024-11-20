using System.Security.Cryptography;
using System.Text;

namespace Luna.Tools.Crypto;

public static class Crypto
{
    public static async Task<byte[]> HashSha512Async(string value)
    {
        using var hashAlg = SHA512.Create();;

        var valueBytes = Encoding.UTF8.GetBytes(value);

        Stream valueStream = new MemoryStream(valueBytes);

        return await hashAlg.ComputeHashAsync(valueStream);
    }

    public static byte[] HashSha512(string value)
    {
        using var hashAlg = SHA512.Create();;

        var valueBytes = Encoding.UTF8.GetBytes(value);

        return hashAlg.ComputeHash(valueBytes);
    }
}