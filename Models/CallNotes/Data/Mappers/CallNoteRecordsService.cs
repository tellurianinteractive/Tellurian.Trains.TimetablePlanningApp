using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Odbc;

namespace TimetablePlanning.Models.CallNotes.Data.Mappers;

public abstract class CallNoteRecordsService() : ICallNoteRecordsService
{
    protected abstract IDbConnection CreateConnection();
    protected abstract IDbCommand CreateCommand(string sql, IDbConnection connection);

    public Task<IEnumerable<LocoConnectRecord>> GetLocoConnectRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "LocoConnectRecords", LocoRecordMapper.ToLocoConnectRecord);

    public Task<IEnumerable<LocoDisconnectRecord>> GetLocoDisconnectRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "LocoDisconnectRecords", LocoRecordMapper.ToLocoDisconnectRecord);

    public Task<IEnumerable<LocoExchangeRecord>> GetLocoExchangeRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "LocoExchangeRecords", LocoRecordMapper.ToLocoExchangeRecord);

    public Task<IEnumerable<LocoTurnOrCirculateRecord>> GetLocoTurnOrCirculateRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "LocoTurnOrCirculationRecords", LocoRecordMapper.ToLocoTurnOrCirculateRecord);

    public Task<IEnumerable<ManualNoteRecord>> GetManualNoteRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "ManualNoteRecords", ManualNoteRecordMapper.ToManualNoteRecord);

    public Task<IEnumerable<ScheduledWagonsConnectRecord>> GetScheduledWagonsConnectRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "ScheduledWagonsConnectRecords", ScheduledWagonsRecordMapper.ToScheduledWagonsConnectNote);

    public Task<IEnumerable<ScheduledWagonsDisconnectRecord>> GetScheduledWagonsDisconnectRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "ScheduledWagonsDisconnectRecords", ScheduledWagonsRecordMapper.ToScheduledWagonsDisconnectNote);

    public Task<IEnumerable<TrainMeetRecord>> GetTrainMeetRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "TrainMeetRecords", TrainMeetRecordMapper.ToTrainMeetRecord);

    public Task<IEnumerable<WagonGroupConnectRecord>> GetWagonGroupsConnectRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "WagonGroupsConnectRecords", WagonGroupRecordMapper.ToWagonGroupConnectRecord);

    public Task<IEnumerable<WagonGroupDisconnectRecord>> GetWagonGroupsDisconnectRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "WagonGroupsDisconnectRecords", WagonGroupRecordMapper.ToWagonGroupDisconnectRecord);

    public Task<IEnumerable<WagonGroupFromCustomersRecord>> GetWagonGroupFromCustomersRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "WagonGroupFromCustomersRecords", WagonGroupRecordMapper.ToWagonGroupFromCustomersRecord);

    public Task<IEnumerable<WagonGroupToCustomersRecord>> GetWagonGroupToCustomersRecordsAsync(int layoutId) =>
        GetRecordsAsync(layoutId, "WagonGroupToCustomersRecords", WagonGroupRecordMapper.ToWagonGroupToCustomersRecord);


    private Task<IEnumerable<TRecord>> GetRecordsAsync<TRecord>(int layoutId, string viewName, Func<IDataRecord, TRecord> mapper)
    {
        using var connection = CreateConnection();
        var countSql = $"SELECT COUNT(*) FROM {viewName} WHERE LayoutId = {layoutId}";
        var countCommand = CreateCommand(countSql, connection);
        var dataSql = $"SELECT * FROM {viewName} WHERE LayoutId = {layoutId}";
        var dataCommand = CreateCommand(dataSql, connection);
        connection.Open();
        var count = countCommand.ExecuteScalar();
        if (count is int c)
        {
            var result = new List<TRecord>(c);
            var reader = dataCommand.ExecuteReader();
            while (reader.Read()) result.Add(mapper(reader));
            connection.Close();
            return Task.FromResult(result.AsEnumerable());
        }
        return Task.FromResult(Enumerable.Empty<TRecord>());
    }

}

public sealed class OdbcCallNoteRecordsService(string connectionString) : CallNoteRecordsService()
{
    protected override OdbcConnection CreateConnection() => new(connectionString);
    protected override OdbcCommand CreateCommand(string sql, IDbConnection connection) => new (sql, (OdbcConnection)connection);
}

public sealed class SqlCallNoteRecordsService(string connectionString) : CallNoteRecordsService()
{
    protected override SqlConnection CreateConnection() => new(connectionString);
    protected override SqlCommand CreateCommand(string sql, IDbConnection connection) => new(sql, (SqlConnection)connection);
}

