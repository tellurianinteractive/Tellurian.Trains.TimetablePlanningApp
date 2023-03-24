using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Extensions;

internal static class LocoCallNoteExtensions {
    public static IEnumerable<LocoConnectNote> AsLocoConnectNotes(this IEnumerable<LocoConnectEvent> events) =>
        events.Select(e => e.AsLocoConnectioNote());

    public static LocoConnectNote AsLocoConnectioNote(this LocoConnectEvent e) =>
        new()
        {
            ForCallId = e.CallId,
            LocoInfo = e.AsLocoInfo(),
            TrainOperationDays = e.TrainOperatingDaysFlags.AsOperationDays(),
            DutyOperationDays = e.DutyOperatingDaysFlags.AsOperationDays(),
            LocoOperationDays = e.LocoOperatingDaysFlags.AsOperationDays(),
            CollectFromStagingArea = e.CollectFromStagingArea,
        };

    public static IEnumerable<LocoDisconnectNote> AsLocoDisconnectNotes(this IEnumerable<LocoDisconnectEvent> events) =>
        events.Select(ld => ld.AsLocoDisconnectNote());

    public static LocoDisconnectNote AsLocoDisconnectNote(this LocoDisconnectEvent e) =>
         new()
         {
             ForCallId = e.CallId,
             LocoInfo = e.AsLocoInfo(),
             TrainOperationDays = e.TrainOperatingDaysFlags.AsOperationDays(),
             DutyOperationDays = e.DutyOperatingDaysFlags.AsOperationDays(),
             LocoOperationDays = e.LocoOperatingDaysFlags.AsOperationDays(),
             DriveToStagingArea = e.DriveToStagingArea,
             CirculateLoco = e.CirculateLoco,
             TurnLoco = e.TurnLoco,
         };
    public static IEnumerable<LocoExchangeNote> AsLocoExchangeNotes(this IEnumerable<LocoExchangeEvent> events) =>
        events.Select(e => e.AsLocoExchangeNote());

    public static LocoExchangeNote AsLocoExchangeNote(this LocoExchangeEvent e)
    {
        return new()
        {
            ForCallId = e.CallId,
            DutyOperationDays = e.DutyOperatingDaysFlags.AsOperationDays(),
            TrainOperationDays = e.TrainOperatingDaysFlags.AsOperationDays(),
            LocoOperationDays = e.LocoOperatingDaysFlags.AsOperationDays(),
            ReplacingLocoOperationDays = e.ReplacingLocoOperatingDaysFlags.AsOperationDays(),
        };
    }

    private static LocoInfo AsLocoInfo(this LocoEvent e) =>
        new()
        {
            Class = e.LocoClass,
            OperatorSignature = e.LocoOperatorSignature,
            LocoNumber = e.LocoNumber ?? string.Empty,
            TurnusNumber = e.TurnusNumber,
            IsDoubleDirectionTrain = e.IsDoubleDirectionTrain
        };
}




