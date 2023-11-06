using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;

internal static class LocoCallNoteMappings
{
    public static IEnumerable<LocoConnectNote> ToLocoConnectNotes(this IEnumerable<LocoConnectRecord> events) =>
        events.Select(e => e.ToLocoConnectioNote());

    public static LocoConnectNote ToLocoConnectioNote(this LocoConnectRecord e) =>
        new()
        {
            ForCallId = e.CallId,
            LocoInfo = e.ToLocoInfo(),
            TrainOperationDays = e.TrainOperatingDaysFlags.ToOperationDays(),
            DutyOperationDays = e.DutyOperatingDaysFlags.ToOperationDays(),
            LocoOperationDays = e.LocoOperatingDaysFlags.ToOperationDays(),
            CollectFromStagingArea = e.CollectFromStagingArea,
        };

    public static IEnumerable<LocoDisconnectNote> ToLocoDisconnectNotes(this IEnumerable<LocoDisconnectRecord> events) =>
        events.Select(ld => ld.ToLocoDisconnectNote());

    public static LocoDisconnectNote ToLocoDisconnectNote(this LocoDisconnectRecord e) =>
         new()
         {
             ForCallId = e.CallId,
             LocoInfo = e.ToLocoInfo(),
             TrainOperationDays = e.TrainOperatingDaysFlags.ToOperationDays(),
             DutyOperationDays = e.DutyOperatingDaysFlags.ToOperationDays(),
             LocoOperationDays = e.LocoOperatingDaysFlags.ToOperationDays(),
             DriveToStagingArea = e.DriveToStagingArea,
             CirculateLoco = e.CirculateLoco,
             TurnLoco = e.TurnLoco,
         };
    public static IEnumerable<LocoExchangeNote> ToLocoExchangeNotes(this IEnumerable<LocoExchangeRecord> events) =>
        events.Select(e => e.ToLocoExchangeNote());

    public static LocoExchangeNote ToLocoExchangeNote(this LocoExchangeRecord e)
    {
        return new()
        {
            ForCallId = e.CallId,
            DutyOperationDays = e.DutyOperatingDaysFlags.ToOperationDays(),
            TrainOperationDays = e.TrainOperatingDaysFlags.ToOperationDays(),
            LocoOperationDays = e.LocoOperatingDaysFlags.ToOperationDays(),
            ReplacingLocoOperationDays = e.ReplacingLocoOperatingDaysFlags.ToOperationDays(),
        };
    }

    private static LocoInfo ToLocoInfo(this LocoRecord e) =>
        new()
        {
            Class = e.LocoClass,
            OperatorSignature = e.LocoOperatorSignature,
            LocoNumber = e.LocoNumber ?? string.Empty,
            TurnusNumber = e.TurnusNumber,
            IsDoubleDirectionTrain = e.IsDoubleDirectionTrain
        };
}




