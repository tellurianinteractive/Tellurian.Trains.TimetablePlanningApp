using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Mappings;

internal static class LocoCallNoteMappings
{
    public static IEnumerable<LocoConnectCallNote> ToLocoConnectNotes(this IEnumerable<LocoConnectRecord> records) =>
        records.Select(e => e.ToLocoConnectioNote());

    public static LocoConnectCallNote ToLocoConnectioNote(this LocoConnectRecord record) =>
        new()
        {
            ForCallId = record.CallId,
            LocoInfo = record.ToLocoInfo(),
            TrainOperationDays = record.TrainOperationDaysFlags.ToOperationDays(),
            DutyOperationDays = record.DutyOperationDaysFlags.ToOperationDays(),
            LocoOperationDays = record.LocoOperationDaysFlags.ToOperationDays(),
            CollectFromStagingArea = record.CollectFromStagingArea,
        };

    public static IEnumerable<LocoDisconnectCallNote> ToLocoDisconnectNotes(this IEnumerable<LocoDisconnectRecord> records) =>
        records.Select(ld => ld.ToLocoDisconnectNote());

    public static LocoDisconnectCallNote ToLocoDisconnectNote(this LocoDisconnectRecord record) =>
         new()
         {
             ForCallId = record.CallId,
             LocoInfo = record.ToLocoInfo(),
             TrainOperationDays = record.TrainOperationDaysFlags.ToOperationDays(),
             DutyOperationDays = record.DutyOperationDaysFlags.ToOperationDays(),
             LocoOperationDays = record.LocoOperationDaysFlags.ToOperationDays(),
             DriveToStagingArea = record.DriveToStagingArea,
         };
    public static IEnumerable<LocoExchangeCallNote> ToLocoExchangeNotes(this IEnumerable<LocoExchangeRecord> records) =>
        records.Select(e => e.ToLocoExchangeNote());

    public static LocoExchangeCallNote ToLocoExchangeNote(this LocoExchangeRecord record)
    {
        return new()
        {
            ForCallId = record.CallId,
            DutyOperationDays = record.DutyOperationDaysFlags.ToOperationDays(),
            TrainOperationDays = record.TrainOperationDaysFlags.ToOperationDays(),
            LocoOperationDays = record.LocoOperationDaysFlags.ToOperationDays(),
            ReplacingLocoOperationDays = record.ReplacingLocoOperationDaysFlags.ToOperationDays(),
            ReplacingLocoOperatorSignature = record.ReplacingLocoOperatorSignature,
            ReplacingLocoClass = record.ReplacingLocoClass,
            ReplacingLocoNumber = record.ReplacingLocoNumber,
        };
    }

    public static IEnumerable<LocoTurnOrCirculateCallNote> ToLocoTurnOrCirculateCallNotes(this IEnumerable<LocoTurnOrCirculateRecord> records) =>
        records.Where(r => !r.IsDoubleDirection).Select(r => r.ToLocoTurnOrCirculateCallNote());
    public static LocoTurnOrCirculateCallNote ToLocoTurnOrCirculateCallNote(this LocoTurnOrCirculateRecord record) =>
        new()
        {
            ForCallId = record.CallId,
            DutyOperationDays = record.DutyOperationDaysFlags.ToOperationDays(),
            TrainOperationDays = record.TrainOperationDaysFlags.ToOperationDays(),
            LocoOperationDays = record.LocoOperationDaysFlags.ToOperationDays(),
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
        };
}




