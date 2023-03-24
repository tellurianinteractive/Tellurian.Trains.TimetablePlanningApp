using System.Collections.Generic;
using System.Threading.Tasks;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes.Services;

/// <summary>
/// Service to load all <see cref="TrainCallNote"/> from a datasource using a <see cref="ICallEventsService"/>.
/// </summary>
public class CallNotesService {

    private readonly ICallEventsService CallEventsService;

    public CallNotesService(ICallEventsService callEventsService)
    {
        CallEventsService = callEventsService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="layoutId">The ID for the layout schedule toload notes.</param>
    /// <returns>All <see cref="TrainCallNote"/> for trains in a specific layout.</returns>
    /// <remarks>
    /// This servvice is deliberately not implemented as <see cref="IAsyncEnumerable{T}"/>
    /// because all notes needs to be loaded before processing them.
    /// </remarks>
    public async Task<IEnumerable<TrainCallNote>> GetCallNotesAsync(int layoutId)
    {
        var result = new List<TrainCallNote>(200);
        result.AddRange((await CallEventsService.GetLocoConnectEventsAsync(layoutId)).AsLocoConnectNotes());
        result.AddRange((await CallEventsService.GetLocoDisconnectEventsAsync(layoutId)).AsLocoDisconnectNotes());
        result.AddRange((await CallEventsService.GetLocoExchangeEventsAsync(layoutId)).AsLocoExchangeNotes());
        result.AddRange((await CallEventsService.GetScheduledWagonsConnectEventsAsync(layoutId)).AsScheduledWagonsConnectNotes());
        result.AddRange((await CallEventsService.GetScheduledWagonsDisconnectEventsAsync(layoutId)).AsScheduledWagonsDisconnectNotes());
        result.AddRange((await CallEventsService.GetTrainMeetEventsAsync(layoutId)).AsTrainMeetNotes());
        result.AddRange((await CallEventsService.GetWaybillWagonsConnectEventsAsync(layoutId)).AsWaybillWagonsConnectNotes());
        result.AddRange((await CallEventsService.GetWaybillWagonsDisconnectEventsAsync(layoutId)).AsWaybillWagonsDisconnectNotes());
        return result;
    }
}
