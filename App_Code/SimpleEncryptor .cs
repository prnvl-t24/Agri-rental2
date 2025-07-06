using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SimpleEncryptor
/// </summary>
public class SimpleEncryptor
{
    public SimpleEncryptor()
    { }
         private static readonly byte key = 0x5A; // simple XOR key

    public static string Encrypt(string input)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
        for (int i = 0; i < bytes.Length; i++)
            bytes[i] ^= key;

        return Convert.ToBase64String(bytes);
    }

    public static string Decrypt(string encrypted)
    {
        byte[] bytes = Convert.FromBase64String(encrypted);
        for (int i = 0; i < bytes.Length; i++)
            bytes[i] ^= key;

        return System.Text.Encoding.UTF8.GetString(bytes);
    }

}
