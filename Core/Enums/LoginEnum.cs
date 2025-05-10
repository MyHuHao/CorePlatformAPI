namespace Core.Enums;

public static class LoginEnum
{
    public static string ToRegion(this int status)
    {
        return status switch
        {
            0 => "SZ",
            1 => "SH",
            _ => "SZ"
        };
    }

    public static string ToDataBase(this int status)
    {
        return status switch
        {
            0 => "Test",
            1 => "Prod",
            _ => "Test"
        };
    }
}