﻿namespace TimetablePlanning.Models.CallNotes.Data;

public abstract class TrainRecord : NoteRecord
{
    public string? TrainPrefix { get; init; }
    public required string TrainNumber { get; init; }
    public required TimeSpan TrainArrivalTime { get; init; }
    public required TimeSpan TrainDepartureTime { get; init;}

}
