namespace Unistream.Domain.Exceptions.Base;

public abstract class BadRequestException : UnistreamBaseException
{
    protected BadRequestException(string message, string detail)
        : base(message, detail)
    {
    }
}
