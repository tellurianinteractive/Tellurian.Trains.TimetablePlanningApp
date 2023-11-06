using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;

internal static class LocoCallNoteMappings
{
    public static IEnumerable<LocoConnectNote> ToLocoConnectNotes(this IEnumerable<LocoConnectRecord> records) =>
        records.Select(e => e.ToLocoConnectioNote());

    public static LocoConnectNote ToLocoConnectioNote(this LocoConnectRecord record) =>
        new()
        {
            ForCallId = record.CallId,
            LocoInfo = record.ToLocoInfo(),
            TrainOperationDays = record.TrainOperatingDaysFlags.ToOperationDays(),
            DutyOperationDays = record.DutyOperatingDaysFlags.ToOperationDays(),
            LocoOperationDays = record.LocoOperatingDaysFlags.ToOperationDays(),
            CollectFromStagingArea = record.CollectFromStagingArea,
        };

    public static IEnumerable<LocoDisconnectNote> ToLocoDisconnectNotes(this IEnumerable<LocoDisconnectRecord> records) =>
        records.Select(ld => ld.ToLocoDisconnectNote());

    public static LocoDisconnectNote ToLocoDisconnectNote(this LocoDisconnectRecord record) =>
         new()
         {
             ForCallId = record.CallId,
             LocoInfo = record.ToLocoInfo(),
             TrainOperationDays = record.TrainOperatingDaysFlags.ToOperationDays(),
             DutyOperationDays = record.DutyOperatingDaysFlags.ToOperationDays(),
             LocoOperationDays = record.LocoOperatingDaysFlags.ToOperationDays(),
             DriveToStagingArea = record.DriveToStagingArea,
         };
    public static IEnumerable<LocoExchangeNote> ToLocoExchangeNotes(this IEnumerable<LocoExchangeRecord> records) =>
        records.Select(e => e.ToLocoExchangeNote());

    public static LocoExchangeNote ToLocoExchangeNote(this LocoExchangeRecord record)
    {
        return new()
        {
            ForCallId = record.CallId,
            DutyOperationDays = record.DutyOperatingDaysFlags.ToOperationDays(),
            TrainOperationDays = record.TrainOperatingDaysFlags.ToOperationDays(),
            LocoOperationDays = record.LocoOperatingDaysFlags.ToOperationDays(),
            ReplacingLocoOperationDays = record.ReplacingLocoOperatingDaysFlags.ToOperationDays(),
        };
    }

    public static IEnumerable<LocoTurnOrCirculateCallNote> ToLocoTurnOrCirculateCallNotes(this IEnumerable<LocoTurnOrCirculateRecord> records) =>
        records.Where(r => !r.IsDoubleDirection).Select(r => r.ToLocoTurnOrCirculateCallNote());
    public static LocoTurnOrCirculateCallNote ToLocoTurnOrCirculateCallNote(this LocoTurnOrCirculateRecord record) =>
        new()
        {
            ForCallId = record.CallId,
            DutyOperationDays = record.DutyOperatingDaysFlags.ToOperationDays(),
            TrainOperationDays = record.TrainOperatingDaysFlags.ToOperationDays(),
            LocoOperationDays = record.LocoOperatingDaysFlags.ToOperationDays(),
            CirculateLoco = record.CirculateLoco,
            TurnLoco = record.TurnLoco,
        };

    private static LocoInfo ToLocoInfo(this LocoRecord record) =>
        new()
        {
            Class = record.LocoClass,
            OperatorSignature = record.LocoOperatorSignature,
            LocoNumber = record.LocoNumber ?? string.Empty,
            TurnusNumber = record.TurnusNumber,
            IsDoubleDirectionTrain = record.IsDoubleDirectionTrain
        };
}




