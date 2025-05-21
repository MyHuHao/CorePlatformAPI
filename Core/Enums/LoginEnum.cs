namespace Core.Enums;

public static class LoginEnum
{
    public static string ToRegion(this int status)
    {
        return status switch
        {
            0 => "COMP1001",
            1 => "COMP1001",
            2 => "COMP3003",
            3 => "COMP3003",
            _ => "COMP1001"
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