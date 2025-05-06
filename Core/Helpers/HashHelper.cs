using System.Security.Cryptography;
using System.Text;

namespace Core.Helpers;

public class HashHelper
{
    /// <summary>
    /// 生成密码哈希与盐值
    /// </summary>
    /// <param name="password">明文密码</param>
    /// <param name="passwordHash">输出哈希值（Base64字符串）</param>
    /// <param name="passwordSalt">输出盐值（Base64字符串）</param>
    public static void GeneratePasswordHash(string password, out string passwordHash, out string passwordSalt)
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
        passwordHash = Convert.ToBase64String(hash); // 存储为 Base64 字符串
        passwordSalt = Convert.ToBase64String(salt); // 存储盐值
    }

    /// <summary>
    /// 验证密码是否匹配存储的哈希值
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

        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(saltedPassword);
        var inputHash = Convert.ToBase64String(hash);
        return inputHash == storedHash; // 比对哈希值 [[6]]
    }

    // 辅助方法：合并字节数组
    private static byte[] Combine(byte[] first, byte[] second)
    {
        var combined = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, combined, 0, first.Length);
        Buffer.BlockCopy(second, 0, combined, first.Length, second.Length);
        return combined;
    }
}