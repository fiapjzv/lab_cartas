using System;

public interface IPlayerAccount
{
    Guid Id { get; }
}

public class PlayerAccount : IPlayerAccount
{
    public Guid Id { get; } = Guid.NewGuid();
}
