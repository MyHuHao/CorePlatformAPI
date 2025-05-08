namespace Core.Exceptions
{
    // 抽象基类，C# 12 主构造函数
    public abstract class BaseApiException(int msgCode, string message)
        : Exception(message)
    {
        public int MsgCode { get; } = msgCode;
        public string Msg { get; } = message;
    }

    // 各种 HTTP 异常类型，均需写空类体 {}
    public class BadRequestException2(int msgCode, string message) : BaseApiException(msgCode, message);

    public class UnauthorizedException2(int msgCode, string message) : BaseApiException(msgCode, message);

    public class ValidationException2(int msgCode, string message) : BaseApiException(msgCode, message);

    public class NotFoundException2(int msgCode, string message) : BaseApiException(msgCode, message);
}