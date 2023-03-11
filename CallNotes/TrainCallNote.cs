using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes;

public abstract class TrainCallNote
{
    public int ForCallId { get; init; }
    public bool IsForArrival { get; init; }
    public bool IsForDeparture { get; init; }
    public bool IsToLocoDriver { get; init; }
    public bool IsToShunter { get; init; }
    public bool IsToDispatcher { get; init; }
    public required OperationDays TrainOperationDays { get; init; }
    public required OperationDays DutyOperationDays { get; init; }
    protected OperationDays ServiceOperationDays => TrainOperationDays & DutyOperationDays;

    public abstract MarkupString AsMarkup();
}

