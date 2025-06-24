namespace Unistream.Domain.Exceptions.Base;

public abstract class UnistreamBaseException : Exception
{
    public string Detail { get; set; }

    protected UnistreamBaseException(string message, string detail)
        : base(message)
    {
        Detail = detail;
    }
}
