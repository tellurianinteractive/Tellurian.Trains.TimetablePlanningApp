using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Utilities.Extensions;

namespace TimetablePlanning.Models.CallNotes;

public abstract class BlockNote : TrainCallNote
{
}
public sealed class BlockDisconnectNote : BlockNote
{
    public IEnumerable<BlockInfo> Destinations { get; internal set; } = Enumerable.Empty<BlockInfo>();
    public override MarkupString AsMarkup() => new(string.Concat(UncoupleMarkup, ContinueMarkup));
    private string UncoupleMarkup => HasWagonsToUncouple ? $"{Resources.Notes.UncoupleWagonsTo.TextMarkup()} {string.Join("", UncoupleDestinations.Select(d => d.Destination.Markup))}" : string.Empty;
    private string ContinueMarkup => HasContinuingWagons ? $"{Resources.Notes.ContinuingWagonsTo.TextMarkup()} {string.Join("", ContinuingDestinations.Select(d => d.Markup))}" : string.Empty;

    private static Func<BlockInfo, bool> WagonsToUncouple => blockInfo => blockInfo.UncoupleHere;
    private static Func<BlockInfo, bool> ContinuingWagons => blockInfo => blockInfo.IsContinued && blockInfo.ShowContinued;
    private bool HasWagonsToUncouple => Destinations.Where(WagonsToUncouple).Any();
    private bool HasContinuingWagons => Destinations.Where(ContinuingWagons).Any();
    private IEnumerable<BlockInfo> UncoupleDestinations => Destinations.Where(WagonsToUncouple).OrderBy(d => d.PositionInTrain);
    private IEnumerable<BlockInfo> ContinuingDestinations => Destinations.Where(ContinuingWagons).OrderBy(d => d.PositionInTrain);
}

public sealed class BlockInfo
{
    public required int PositionInTrain { get; init; }
    public bool UncoupleHere { get; init; }
    public bool IsContinued => !UncoupleHere;
    public bool ShowContinued { get; init; }
    public required DestinationInfo Destination { get; init; }
    public required OriginInfo Origin { get; init; }
    public string Markup => string.Concat(Resources.Notes.From.MarkupSpan(), Origin.Markup, Resources.Notes.To.MarkupSpan(), Destination.Markup);
}

public sealed class DestinationInfo
{
    public required string FullName { get; init; }
    public required string ForeColor { get; init; }
    public required string BackColor { get; init; }
    public string Markup => $"""
        <span class="note-destination" style="color: {ForeColor};background-color: {BackColor};">{FullName}</span>
        """;


}
public sealed class OriginInfo
{
    public required string FullName { get; init; }
    public string Markup => $"""
        <span class="note-origin">{FullName}</span>
        """;


}
