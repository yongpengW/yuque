namespace Yuque.Application.Abstractions;

public interface ICurrentUser
{
    long? UserId { get; }
}
