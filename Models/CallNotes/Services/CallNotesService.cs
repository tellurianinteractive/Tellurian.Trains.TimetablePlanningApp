using System.Runtime.CompilerServices;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Mappings;

[assembly:InternalsVisibleTo("TimetablePlanning.Models.CallNotes.Tests")]


namespace TimetablePlanning.Models.CallNotes.Services;

/// <summary>
/// Service to load all <see cref="TrainCallNote"/> from a datasource using a <see cref="ICallNoteRecordsService"/>.
/// </summary>
public class CallNotesService(ICallNoteRecordsService callEventsService, int expectedNumberOfCallNotes = 200)
{

    private readonly ICallNoteRecordsService CallEventsService = callEventsService;

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
        var result = new List<TrainCallNote>(expectedNumberOfCallNotes);
        result.AddRange((await CallEventsService.GetLocoConnectRecordsAsync(layoutId)).ToLocoConnectNotes());
        result.AddRange((await CallEventsService.GetLocoDisconnectRecordsAsync(layoutId)).ToLocoDisconnectNotes());
        result.AddRange((await CallEventsService.GetLocoExchangeRecordsAsync(layoutId)).ToLocoExchangeNotes());
        result.AddRange((await CallEventsService.GetLocoTurnOrCirculateRecordsAsync(layoutId)).ToLocoTurnOrCirculateCallNotes());
        result.AddRange((await CallEventsService.GetManualNoteRecordsAsync(layoutId)).ToManualTrainCallNotes());
        result.AddRange((await CallEventsService.GetScheduledWagonsConnectRecordsAsync(layoutId)).ToScheduledWagonsConnectNotes());
        result.AddRange((await CallEventsService.GetScheduledWagonsDisconnectRecordsAsync(layoutId)).ToScheduledWagonsDisconnectNotes());
        result.AddRange((await CallEventsService.GetTrainMeetsRecordsAsync(layoutId)).ToTrainMeetCallNotes());
        result.AddRange((await CallEventsService.GetTrainPassesRecordsAsync(layoutId)).ToTrainPassingCallNotes());
        result.AddRange((await CallEventsService.GetWagonGroupsConnectRecordsAsync(layoutId)).ToWagonGroupsConnectNotes());
        result.AddRange((await CallEventsService.GetWagonGroupsDisconnectRecordsAsync(layoutId)).ToWagonGroupsDisconnectNotes());
        result.AddRange((await CallEventsService.GetWagonGroupFromCustomersRecordsAsync(layoutId)).ToWagonGroupFromCustomersCallNotes());
        result.AddRange((await CallEventsService.GetWagonGroupToCustomersRecordsAsync(layoutId)).ToWagonGroupToCustomersCallNote());
        return result;
    }
}
