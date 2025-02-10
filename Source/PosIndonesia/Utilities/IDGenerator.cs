using System;
using System.Linq;

namespace PosIndonesia.Utilities;
public sealed class IDGenerator
{
    const string Encode_32_Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUV";
    static readonly char[] _prefix = new char[6];
    static long _lastId = DateTime.UtcNow.Ticks;

    static IDGenerator() => PopulatePrefix();
    IDGenerator() { }

    public static IDGenerator Instance { get; } = new IDGenerator();

    public static string NewId() => GenerateImpl(Interlocked.Increment(ref _lastId));

    static string GenerateImpl(long id)
    {
        char[] buffer = new char[20];

        // Copy prefix
        for (int i = 0; i < _prefix.Length; i++)
        {
            buffer[i] = _prefix[i];
        }
        buffer[6] = '-';

        buffer[7] = Encode_32_Chars[(int)(id >> 60) & 31];
        buffer[8] = Encode_32_Chars[(int)(id >> 55) & 31];
        buffer[9] = Encode_32_Chars[(int)(id >> 50) & 31];
        buffer[10] = Encode_32_Chars[(int)(id >> 45) & 31];
        buffer[11] = Encode_32_Chars[(int)(id >> 40) & 31];
        buffer[12] = Encode_32_Chars[(int)(id >> 35) & 31];
        buffer[13] = Encode_32_Chars[(int)(id >> 30) & 31];
        buffer[14] = Encode_32_Chars[(int)(id >> 25) & 31];
        buffer[15] = Encode_32_Chars[(int)(id >> 20) & 31];
        buffer[16] = Encode_32_Chars[(int)(id >> 15) & 31];
        buffer[17] = Encode_32_Chars[(int)(id >> 10) & 31];
        buffer[18] = Encode_32_Chars[(int)(id >> 5) & 31];
        buffer[19] = Encode_32_Chars[(int)id & 31];

        return new string(buffer);
    }

    static void PopulatePrefix()
    {
        // Generate a random prefix since Environment.MachineName is unavailable in WASM
        Random random = new();
        for (int i = 0; i < _prefix.Length; i++)
        {
            _prefix[i] = Encode_32_Chars[random.Next(Encode_32_Chars.Length)];
        }
    }
}
