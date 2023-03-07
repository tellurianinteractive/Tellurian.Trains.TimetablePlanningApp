using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Transactions;
using TimetablePlanning.Utilities.Extensions;

namespace TimetablePlanning.Models.CallNotes;

public abstract class TrainCallNote
{
    public int ForCallId { get; init; }
    public required OperationDays TrainOperationDays { get; init; }
    public required OperationDays DutyOperationDays { get; init; }
    public abstract MarkupString AsMarkup();
    protected OperationDays ServiceOperationDays => TrainOperationDays & DutyOperationDays;
    protected string ErrorMarkup(string text) => new(Markup(text, "error"));
    protected string ItemMarkup(string text) => Markup(text, "item");
    protected string TextMarkup(string text) => Markup(text, "text");
    protected string DaysMarkup(string text) => Markup(text, "days");
    protected string Markup(string text, string cssClass = "text") =>
        $"""
        <span class="note-{cssClass}">{text} </span>
        """;
    protected const char Dot = '.';
}

public abstract class BlockNote : TrainCallNote
{

}

public sealed class BlockArrivalNote : BlockNote
{
    public IEnumerable<BlockInfo> Destinations { get; internal set; } = Enumerable.Empty<BlockInfo>();
    public override MarkupString AsMarkup() => new(UncoupleMarkup);
    private string UncoupleMarkup => HasWagonsToUncouple ? $"{TextMarkup(Resources.Notes.UncoupleWagonsTo)} {string.Join("", UncoupleDestinations.Select(d => d.DestinationMarkup))}" : string.Empty;
    private string ContinueMarkup => HasContinuingWagons ? $"{TextMarkup(Resources.Notes.ContinuingWagonsTo)} {string.Join("", ContinuingDestinations.Select(d => d.Markup))}" : string.Empty;

    private bool HasWagonsToUncouple => Destinations.Where(d => d.UncoupleHere).Any();
    private bool HasContinuingWagons => Destinations.Where(d => d.IsContinued).Any();
    private IEnumerable<BlockInfo> UncoupleDestinations => Destinations.Where(d => d.UncoupleHere).OrderBy(d => d.PositionInTrain);
    private IEnumerable<BlockInfo> ContinuingDestinations => Destinations.Where(d => d.IsContinued).OrderBy(d => d.PositionInTrain);
}

public sealed class BlockInfo
{
    public int PositionInTrain { get; init; }
    public bool UncoupleHere { get; init; }
    public bool IsContinued => !UncoupleHere;
    public required string DestinationName { get; init; }
    public required string OriginName { get; init; }
    public required string DestinationForeColor { get; init; }
    public required string DestinationBackColor { get; init; }

    public string DestinationMarkup => $"""
        <span class="note-destination" style="color: {DestinationForeColor};background-color: {DestinationBackColor};">{DestinationName}</span>
        """;
    public string OriginMarkup => $"""
        <span class="note-origin">{OriginName}</span>
        """;
    public string Markup => string.Concat(Resources.Notes.From.MarkupSpan(), OriginMarkup, Resources.Notes.To.MarkupSpan(), DestinationMarkup);
}

public sealed class OriginInfo
{
    public required string FullName { get; init; }

}