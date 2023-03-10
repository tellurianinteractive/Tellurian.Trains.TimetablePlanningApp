using System.Collections.Generic;
using System.Linq;
using TimetablePlanning.Models.CallNotes.Data;

namespace TimetablePlanning.Models.CallNotes.Extensions;

public static class LocoCallNoteExtensions
{
    public static IEnumerable<LocoConnectNote> AsLocoConnectNotes(this IEnumerable<LocoConnectEvent> locoAssignments) =>
        locoAssignments.Select(la => la.AsLocoConnectioNote());

    public static LocoConnectNote AsLocoConnectioNote(this LocoConnectEvent locoConnect) =>
        new ()
        {
            ForCallId = locoConnect.CallId,
            LocoInfo = locoConnect.AsLocoInfo(),
            TrainOperationDays = locoConnect.TrainOperatingDaysFlags.AsOperationDays(),
            DutyOperationDays = locoConnect.DutyOperatingDaysFlags.AsOperationDays(),
            CollectFromStagingArea = locoConnect.CollectFromStagingArea,
        };

    public static IEnumerable<LocoDisconnectNote> AsLocoDisconnectNotes(this IEnumerable<LocoDisconnectEvent> locoDisconnects) =>
        locoDisconnects.Select(ld => ld.AsLocoDisconnectNote());

    public static LocoDisconnectNote AsLocoDisconnectNote(this LocoDisconnectEvent locoDisconnect) =>
         new ()
         {
             ForCallId = locoDisconnect.CallId,
             LocoInfo = locoDisconnect.AsLocoInfo(),
             TrainOperationDays = locoDisconnect.TrainOperatingDaysFlags.AsOperationDays(),
             DutyOperationDays = locoDisconnect.DutyOperatingDaysFlags.AsOperationDays(),
             DriveToStagingArea = locoDisconnect.DriveToStagingArea,
             CirculateLoco = locoDisconnect.CirculateLoco,
             TurnLoco = locoDisconnect.TurnLoco,
         };

    private static LocoInfo AsLocoInfo(this LocoEvent action) =>
        new()
        {
            Class = action.LocoClass,
            OperationDays = action.LocoOperatingDaysFlags.AsOperationDays(),
            OperatorSignature = action.LocoOperatorSignature,
            LocoNumber = action.LocoNumber ?? string.Empty,
            TurnusNumber = action.TurnNumber,
            IsDoubleDirectionTrain = action.IsDoubleDirectionTrain
        };
}


