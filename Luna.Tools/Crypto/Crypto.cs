using System.Security.Cryptography;
using System.Text;

namespace Luna.Tools.Crypto;

public class Crypto
{
    public static async Task<byte[]> HashSha512Async(string value)
    {
        using var sha512 = new SHA512Managed();

        var valueBytes = Encoding.UTF8.GetBytes(value);

        Stream valueStream = new MemoryStream(valueBytes);

        return await sha512.ComputeHashAsync(valueStream);
    }
}