namespace Tracky.Domain.Common;

public abstract record Identity(Guid Value)
{
    public abstract string AsString();
}
