using System.Collections.Generic;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Extensions;

namespace TimetablePlanning.Models.CallNotes.Services;
public class CallNotesService {

    private readonly ICallEventsService CallEventsService;
    public CallNotesService(ICallEventsService callEventsService) {
        CallEventsService = callEventsService;
    }

    public async IAsyncEnumerable<TrainCallNote> GetCallNotesAsync(int layoutId)
    {
        var locoConnectEvents = await CallEventsService.GetLocoConnectEventsAsync(layoutId);
        foreach (var item in locoConnectEvents.AsLocoConnectNotes()) yield return item;

        var locoDisconnectEvents = await CallEventsService.GetLocoDisconnectEventsAsync(layoutId);
        foreach (var item in locoDisconnectEvents.AsLocoDisconnectNotes()) yield return item;
    }
}
