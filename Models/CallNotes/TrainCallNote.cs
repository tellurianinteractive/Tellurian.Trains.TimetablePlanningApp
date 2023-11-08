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
    private OperationDays ServiceOperationDays => TrainOperationDays & DutyOperationDays;
    protected OperationDays OperationDays => DutyOperationDays & TrainOperationDays & NoteDays;
    protected abstract OperationDays NoteDays { get; }
    protected string Days => NoteDays.IsAllOtherDays(ServiceOperationDays) ? string.Empty: OperationDays.ShortName;
    public abstract MarkupString Markup();
}

