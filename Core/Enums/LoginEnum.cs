namespace Core.Enums;

public static class LoginEnum
{
    public static string ToRegion(this int status)
    {
        return status switch
        {
            0 => "SZ",
            1 => "SZ",
            2 => "SH",
            3 => "SH",
            _ => "SZ"
        };
    }

    public static string ToDataBase(this int status)
    {
        return status switch
        {
            0 => "Test",
            1 => "Prod",
            2 => "Test",
            3 => "Prod",
            _ => "Test"
        };
    }
}