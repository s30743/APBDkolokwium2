namespace APBDkolos2.Exceptions;

public class BadRequestEx : Exception
{
    public BadRequestEx()
    {
    }

    public BadRequestEx(string? message) : base(message)
    {
    }
}