using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

public static class KeyGenerator
{
    private static readonly ConcurrentQueue<string> keyQueue = new ConcurrentQueue<string>();

    static KeyGenerator()
    {
        int preAllocatedKeys = 1000;
        for (int i = 0; i < preAllocatedKeys; i++)
        {
            string key = GenerateKey();
            keyQueue.Enqueue(key);
        }
    }

    public static string GetKey()
    {
        string key;
        while (!keyQueue.TryDequeue(out key))
        {
            string newKey = GenerateKey();
            keyQueue.Enqueue(newKey);
        }
        return key;
    }

    private static string GenerateKey()
    {
        int timestamp = TimeStamp.GetTimeStamp();
        string timestampString = timestamp.ToString();
        byte[] timestampBytes = Encoding.UTF8.GetBytes(timestampString);

        SHA256 sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(timestampBytes);

        string key = $"{timestamp}_{Guid.NewGuid()}";

        return key;
    }
}