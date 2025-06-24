namespace Unistream.Domain.Exceptions.Base;

public abstract class NotFoundException : UnistreamBaseException
{
    protected NotFoundException(string message, string detail)
        : base(message, detail)
    {
    }
}
