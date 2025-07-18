﻿using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers;

public class HashHelper
{
    /// <summary>
    ///     生成密码哈希与盐值
    /// </summary>
    /// <param name="password">明文密码</param>
    public static (string passwordHash, string passwordSalt) GeneratePasswordHash(string password)
    {
        // 生成 32 字节随机盐值 [[8]]
        var salt = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // 密码加盐哈希（SHA256）[[5]]
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltedPassword = Combine(passwordBytes, salt);
        var hash = SHA256.HashData(saltedPassword);
        var passwordHash = Convert.ToBase64String(hash); // 存储为 Base64 字符串
        var passwordSalt = Convert.ToBase64String(salt); // 存储盐值
        return (passwordHash, passwordSalt);
    }

    /// <summary>
    ///     验证密码是否匹配存储的哈希值
    /// </summary>
    /// <param name="password">输入密码</param>
    /// <param name="storedHash">数据库存储的哈希值（Base64）</param>
    /// <param name="storedSalt">数据库存储的盐值（Base64）</param>
    /// <returns>验证结果</returns>
    public static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        // 从 Base64 还原盐值 [[9]]
        var salt = Convert.FromBase64String(storedSalt);

        // 重新计算哈希值
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltedPassword = Combine(passwordBytes, salt);
        var hash = SHA256.HashData(saltedPassword);
        var inputHash = Convert.ToBase64String(hash);
        return inputHash == storedHash; // 比对哈希值 [[6]]
    }

    /// <summary>
    /// 还原密码
    /// </summary>
    /// <param name="storedHash"></param>
    /// <param name="storedSalt"></param>
    /// <returns></returns>
    public static string RestorePassword(string storedHash, string storedSalt)
    {
        // 从 Base64 恢复盐值
        var salt = Convert.FromBase64String(storedSalt);

        // 恢复密码
        var hash = Convert.FromBase64String(storedHash);
        var passwordBytes = new byte[hash.Length - salt.Length];
        Buffer.BlockCopy(hash, salt.Length, passwordBytes, 0, passwordBytes.Length);
        var password = Encoding.UTF8.GetString(passwordBytes);
        return password;
    }

    // 辅助方法：合并字节数组
    private static byte[] Combine(byte[] first, byte[] second)
    {
        var combined = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, combined, 0, first.Length);
        Buffer.BlockCopy(second, 0, combined, first.Length, second.Length);
        return combined;
    }

    // 生成UUID 32 位
    public static string GetUuid()
    {
        var randomBytes = new byte[10];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        var timestamp = DateTime.UtcNow.Ticks / 10000L;
        var timestampBytes = BitConverter.GetBytes(timestamp);
        if (BitConverter.IsLittleEndian) Array.Reverse(timestampBytes);
        var guidBytes = new byte[16];
        Array.Copy(timestampBytes, 2, guidBytes, 0, 6);
        Array.Copy(randomBytes, 0, guidBytes, 6, 10);
        return new Guid(guidBytes).ToString("N");
    }
}