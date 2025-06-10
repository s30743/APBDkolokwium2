namespace APBDkolos2.Exceptions;

public class NotFoundEx : Exception
{
    public NotFoundEx()
    {
    }

    public NotFoundEx(string? message) : base(message)
    {
    }
}