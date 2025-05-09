namespace Core.Enums;

public static class EnumExtensions
{
    // 性别枚举扩展
    public static string ToGenderString(this int gender)
    {
        return gender switch
        {
            0 => "未知",
            1 => "男",
            2 => "女",
            _ => "未知"
        };
    }

    // 状态枚举扩展
    public static string ToStatusString(this int status)
    {
        return status switch
        {
            0 => "禁用",
            1 => "启用",
            _ => "未知"
        };
    }
}