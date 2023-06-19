namespace TimetablePlanning.Models.CallNotes.Data;

/// <summary>
/// This service is used to get raw call event data from a data source.
/// Each class should be mapped to a query or view that contains the data needed.
/// </summary>
/// <remark>Note that each type has different needs for data.</remark>
public interface ICallEventsService {
    Task<IEnumerable<LocoConnectEvent>> GetLocoConnectEventsAsync(int layoutId);
    Task<IEnumerable<LocoDisconnectEvent>> GetLocoDisconnectEventsAsync(int layoutId);
    Task<IEnumerable<LocoExchangeEvent>> GetLocoExchangeEventsAsync(int layoutId);
    Task<IEnumerable<ScheduledWagonsConnectEvent>> GetScheduledWagonsConnectEventsAsync(int layoutId);
    Task<IEnumerable<ScheduledWagonsDisconnectEvent>> GetScheduledWagonsDisconnectEventsAsync(int layoutId);
    Task<IEnumerable<TrainMeetEvent>> GetTrainMeetEventsAsync(int layoutId);
    Task<IEnumerable<WaybillWagonsConnectEvent>> GetWaybillWagonsConnectEventsAsync(int layoutId);
    Task<IEnumerable<WaybillWagonsDisconnectEvent>> GetWaybillWagonsDisconnectEventsAsync(int layoutId);

}
