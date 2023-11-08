namespace TimetablePlanning.Models.CallNotes.Data;

/// <summary>
/// This service is used to get raw call data records from a data source.
/// Each class should be mapped to a query or view that contains the data needed.
/// </summary>
/// <remark>Note that each type has different needs for data.</remark>
public interface ICallNoteRecordsService {
    Task<IEnumerable<LocoConnectRecord>> GetLocoConnectRecordsAsync(int layoutId);
    Task<IEnumerable<LocoDisconnectRecord>> GetLocoDisconnectRecordsAsync(int layoutId);
    Task<IEnumerable<LocoExchangeRecord>> GetLocoExchangeRecordsAsync(int layoutId);
    Task<IEnumerable<LocoTurnOrCirculateRecord>> GetLocoTurnOrCirculateRecordsAsync(int layoutId);
    Task<IEnumerable<ManualNoteRecord>> GetManualNoteRecordsAsync(int layoutId);
    Task<IEnumerable<ScheduledWagonsConnectRecord>> GetScheduledWagonsConnectRecordsAsync(int layoutId);
    Task<IEnumerable<ScheduledWagonsDisconnectRecord>> GetScheduledWagonsDisconnectRecordsAsync(int layoutId);
    Task<IEnumerable<TrainMeetRecord>> GetTrainMeetRecordsAsync(int layoutId);
    Task<IEnumerable<WagonGroupConnectRecord>> GetWagonGroupsConnectRecordsAsync(int layoutId);
    Task<IEnumerable<WagonGroupDisconnectRecord>> GetWagonGroupsDisconnectRecordsAsync(int layoutId);
    Task<IEnumerable<WagonGroupFromCustomersRecord>> GetWagonGroupFromCustomersRecordsAsync(int layoutId);
    Task<IEnumerable<WagonGroupToCustomersRecord>> GetWagonGroupToCustomersRecordsAsync(int layoutId);

}
