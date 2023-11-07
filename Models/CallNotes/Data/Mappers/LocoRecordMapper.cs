using System.Data;
using TimetablePlanning.Models.CallNotes.Data.Mappers.Extensions;

namespace TimetablePlanning.Models.CallNotes.Data.Mappers;
public static class LocoRecordMapper
{
    public static LocoConnectRecord ToLocoConnectRecord(this IDataRecord record) =>
        new()
        {
            CallId = record.GetInt(nameof(LocoRecord.CallId)),
            DutyOperationDaysFlags = record.GetByte(nameof(LocoRecord.DutyOperationDaysFlags)),
            TrainOperationDaysFlags = record.GetByte(nameof(LocoRecord.TrainOperationDaysFlags)),
            LocoOperationDaysFlags = record.GetByte(nameof(LocoRecord.LocoOperationDaysFlags)),
            LocoOperatorSignature = record.GetString(nameof(LocoRecord.LocoOperatorSignature)),
            LocoClass = record.GetString(nameof(LocoRecord.LocoClass)),
            LocoNumber = record.GetString(nameof(LocoRecord.LocoNumber)),
            TurnusNumber = record.GetString(nameof(LocoRecord.TurnusNumber)),
            CollectFromStagingArea = record.GetBool(nameof(LocoConnectRecord.CollectFromStagingArea)),
        };

    public static LocoDisconnectRecord ToLocoDisconnectRecord(this IDataRecord record) =>
        new()
        {
            CallId = record.GetInt(nameof(LocoRecord.CallId)),
            DutyOperationDaysFlags = record.GetByte(nameof(LocoRecord.DutyOperationDaysFlags)),
            TrainOperationDaysFlags = record.GetByte(nameof(LocoRecord.TrainOperationDaysFlags)),
            LocoOperationDaysFlags = record.GetByte(nameof(LocoRecord.LocoOperationDaysFlags)),
            LocoOperatorSignature = record.GetString(nameof(LocoRecord.LocoOperatorSignature)),
            LocoClass = record.GetString(nameof(LocoRecord.LocoClass)),
            LocoNumber = record.GetString(nameof(LocoRecord.LocoNumber)),
            TurnusNumber = record.GetString(nameof(LocoRecord.TurnusNumber)),
            DriveToStagingArea = record.GetBool(nameof(LocoDisconnectRecord.DriveToStagingArea)),
        };

    public static LocoExchangeRecord ToLocoExchangeRecord(this IDataRecord record) =>
         new()
         {
             CallId = record.GetInt(nameof(LocoRecord.CallId)),
             DutyOperationDaysFlags = record.GetByte(nameof(LocoRecord.DutyOperationDaysFlags)),
             TrainOperationDaysFlags = record.GetByte(nameof(LocoRecord.TrainOperationDaysFlags)),
             LocoOperationDaysFlags = record.GetByte(nameof(LocoExchangeRecord.LocoOperationDaysFlags)),
             ReplacingLocoOperationDaysFlags = record.GetByte(nameof(LocoExchangeRecord.ReplacingLocoOperationDaysFlags)),
         };

    public static LocoTurnOrCirculateRecord ToLocoTurnOrCirculateRecord(this IDataRecord record) =>
        new()
        {
            CallId = record.GetInt(nameof(LocoRecord.CallId)),
            DutyOperationDaysFlags = record.GetByte(nameof(LocoRecord.DutyOperationDaysFlags)),
            TrainOperationDaysFlags = record.GetByte(nameof(LocoRecord.TrainOperationDaysFlags)),
            LocoOperationDaysFlags = record.GetByte(nameof(LocoTurnOrCirculateRecord.LocoOperationDaysFlags)),
            IsDoubleDirection = record.GetBool(nameof(LocoTurnOrCirculateRecord.IsDoubleDirection)),
            TurnLoco = record.GetBool(nameof(LocoTurnOrCirculateRecord.TurnLoco)),
            CirculateLoco = record.GetBool(nameof(LocoTurnOrCirculateRecord.CirculateLoco)),
        };

}
        
