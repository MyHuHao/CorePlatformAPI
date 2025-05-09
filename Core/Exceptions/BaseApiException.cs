using Core.Enums;

namespace Core.Exceptions;

public abstract class BaseApiException(MsgCodeEnum msgCode, string message) : Exception(message)
{
    public MsgCodeEnum MsgCode { get; } = msgCode;
}

public class BadRequestException(MsgCodeEnum msgCode, string message) : BaseApiException(msgCode, message);

public class UnauthorizedException(MsgCodeEnum msgCode, string message) : BaseApiException(msgCode, message);

public class ValidationException(MsgCodeEnum msgCode, string message) : BaseApiException(msgCode, message);

public class NotFoundException(MsgCodeEnum msgCode, string message) : BaseApiException(msgCode, message);