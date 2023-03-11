using System.Collections.Generic;
using System.Linq;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Extensions;

public static class LocoCallNoteExtensions
{
    public static IEnumerable<LocoConnectNote> AsLocoConnectNotes(this IEnumerable<LocoConnectEvent> events) =>
        events.Select(e => e.AsLocoConnectioNote());

    public static LocoConnectNote AsLocoConnectioNote(this LocoConnectEvent e) =>
        new ()
        {
            ForCallId = e.CallId,
            LocoInfo = e.AsLocoInfo(),
            TrainOperationDays = e.TrainOperatingDaysFlags.AsOperationDays(),
            DutyOperationDays = e.DutyOperatingDaysFlags.AsOperationDays(),
            CollectFromStagingArea = e.CollectFromStagingArea,
        };

    public static IEnumerable<LocoDisconnectNote> AsLocoDisconnectNotes(this IEnumerable<LocoDisconnectEvent> events) =>
        events.Select(ld => ld.AsLocoDisconnectNote());

    public static LocoDisconnectNote AsLocoDisconnectNote(this LocoDisconnectEvent e) =>
         new ()
         {
             ForCallId = e.CallId,
             LocoInfo = e.AsLocoInfo(),
             TrainOperationDays = e.TrainOperatingDaysFlags.AsOperationDays(),
             DutyOperationDays = e.DutyOperatingDaysFlags.AsOperationDays(),
             DriveToStagingArea = e.DriveToStagingArea,
             CirculateLoco = e.CirculateLoco,
             TurnLoco = e.TurnLoco,
         };

    private static LocoInfo AsLocoInfo(this LocoEvent e) =>
        new()
        {
            Class = e.LocoClass,
            OperationDays = e.LocoOperatingDaysFlags.AsOperationDays(),
            OperatorSignature = e.LocoOperatorSignature,
            LocoNumber = e.LocoNumber ?? string.Empty,
            TurnusNumber = e.TurnNumber,
            IsDoubleDirectionTrain = e.IsDoubleDirectionTrain
        };
}


