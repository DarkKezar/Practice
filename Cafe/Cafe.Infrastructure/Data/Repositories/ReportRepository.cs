using MongoDB.Driver.GridFS;
using Cafe.Application.Interfaces;
using Cafe.Infrastructure.Data.DBContext;
using MongoDB.Bson;

namespace Cafe.Infrastructure.Data.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly IGridFSBucket _reportCollection;

    public ReportRepository(AppDbContext db)
    {
        _reportCollection = db.GetReportCollection();
    }

    public async Task RecordFileAsync(string name, Byte[] bytes, CancellationToken cancellationToken = default)
    {
        await _reportCollection.UploadFromBytesAsync(name, bytes, cancellationToken: cancellationToken);
    }
}
