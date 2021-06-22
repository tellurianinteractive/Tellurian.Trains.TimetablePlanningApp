using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Tellurian.Trains.Scheduling.Model;

namespace Tellurian.Trains.Scheduling.Extensions
{
    public static class StationExtensions
    {
        public static MarkupString NameLabel(this Station me) => new($"{me.Signature}");
        public static MarkupString KmLabel(this Station me) => new( $"{me.Km} km");
        public static string TrackColor(this Station me) => me.HasPassengerExchange ? "gray" : "lightgray";
    }
}
