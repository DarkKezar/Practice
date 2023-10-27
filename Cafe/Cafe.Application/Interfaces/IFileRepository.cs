namespace Cafe.Application.Interfaces;

public interface IFileRepository
{
    Task RecordFileAsync(string name, Byte[] bytes, CancellationToken cancellationToken = default);
}
