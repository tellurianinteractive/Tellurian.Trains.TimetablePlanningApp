using System.Data;
using System.Data.Odbc;

namespace TimetablePlanning.Models.CallNotes.Data.Mappers;

public class AccessCallNoteRecordsService(string connectionString, CancellationTokenSource cancellationTokenSource) : ICallNoteRecordsService
{
    private CancellationTokenSource CancellationTokenSource { get; } = cancellationTokenSource;
    private CancellationToken CancellationToken => CancellationTokenSource.Token;
    private OdbcConnection Connection => new(connectionString);

    public Task<IEnumerable<LocoConnectRecord>> GetLocoConnectRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "LocoConnectRecords", LocoRecordMapper.ToLocoConnectRecord);
    public Task<IEnumerable<LocoDisconnectRecord>> GetLocoDisconnectRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "LocoDisconnectRecords", LocoRecordMapper.ToLocoDisconnectRecord);

    public Task<IEnumerable<LocoExchangeRecord>> GetLocoExchangeRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "LocoExchangeRecords", LocoRecordMapper.ToLocoExchangeRecord);

    public Task<IEnumerable<LocoTurnOrCirculateRecord>> GetLocoTurnOrCirculateRecordsAsync(int layoutId) => throw new NotImplementedException();
    public Task<IEnumerable<ManualNoteRecord>> GetManualNoteRecordsAsync(int layoutId) => throw new NotImplementedException();
    public Task<IEnumerable<ScheduledWagonsConnectRecord>> GetScheduledWagonsConnectRecordsAsync(int layoutId) => throw new NotImplementedException();
    public Task<IEnumerable<ScheduledWagonsDisconnectRecord>> GetScheduledWagonsDisconnectRecordsAsync(int layoutId) => throw new NotImplementedException();
    public Task<IEnumerable<TrainMeetRecord>> GetTrainMeetRecordsAsync(int layoutId) => throw new NotImplementedException();
    public Task<IEnumerable<WagonGroupConnectRecord>> GetWagonGroupsConnectRecordsAsync(int layoutId) => throw new NotImplementedException();
    public Task<IEnumerable<WagonGroupDisconnectRecord>> GetWagonGroupsDisconnectRecordsAsync(int layoutId) => throw new NotImplementedException();


    private async Task<IEnumerable<TRecord>> GetRecordsAsync<TRecord>(int layoutId, string viewName, Func<IDataRecord, TRecord> mapper)
    {
        using var connection = Connection;
        var countSql = $"SELECT COUNT(*) FROM {viewName} WHERE LayoutId = {layoutId}";
        var countCommand = new OdbcCommand(countSql, connection);
        var dataSql = $"SELECT * FROM {viewName} WHERE LayoutId = {layoutId}";
        var dataCommand = new OdbcCommand(dataSql, connection);
        await connection.OpenAsync(CancellationToken).ConfigureAwait(false);
        var count = await countCommand.ExecuteScalarAsync(CancellationToken).ConfigureAwait(false);
        if (count is int c)
        {
            var result = new List<TRecord>(c);
            var reader = await dataCommand.ExecuteReaderAsync(CancellationToken).ConfigureAwait(false);
            while (await reader.ReadAsync()) result.Add(mapper(reader));
            await connection.CloseAsync().ConfigureAwait(false);
            return result;
        }
        return Enumerable.Empty<TRecord>();
    }
}
