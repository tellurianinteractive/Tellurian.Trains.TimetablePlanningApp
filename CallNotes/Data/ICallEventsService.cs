using System.Collections.Generic;
using System.Threading.Tasks;

namespace TimetablePlanning.Models.CallNotes.Data;

/// <summary>
/// This service is used to get raw call event data from a data source.
/// Each class should be mapped to a query or view that contains the data needed.
/// <remark>Note that each type has different needs for data.</remark>
/// </summary>
public interface ICallEventsService
{
    Task<IEnumerable<BlockConnectEvent>> GetBlockConnectEventsAsync(int layoutId);
    Task<IEnumerable<BlockDisconnectEvent>> GetBlockDisconnectEventsAsync(int layoutId);
    Task<IEnumerable<LocoConnectEvent>> GetLocoConnectEventsAsync(int layoutId);
    Task<IEnumerable<LocoDisconnectEvent>> GetLocoDisconnectEventsAsync(int layoutId);
    Task<IEnumerable<TrainMeetEvent>> GetTrainMeetEventsAsync(int layoutId);

}
