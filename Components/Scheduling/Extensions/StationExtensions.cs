﻿using Microsoft.AspNetCore.Components;
using TimetablePlanning.Components.Scheduling;

namespace TimetablePlanning.Components.Scheduling.Extensions; 
    public static class StationExtensions
    {
        public static MarkupString NameLabel(this Station me) => new($"{me.Signature}");
        public static MarkupString KmLabel(this Station me) => new( $"{me.Km} km");
        public static string TrackColor(this Station me) => me.HasPassengerExchange ? "gray" : "lightgray";
    }