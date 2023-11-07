using System.Data;
using System.Text;
using TimetablePlanning.Models.CallNotes.Data.Mappers.Extensions;

namespace TimetablePlanning.Models.CallNotes.Data.Mappers;
public static class ScheduledWagonsRecordMapper
{
    public static ScheduledWagonsConnectRecord ToScheduledWagonsConnectNote(this IDataRecord record) =>
        new() {
            CallId = record.GetInt(nameof(ScheduledWagonsRecord.CallId)),
            DutyOperationDaysFlags = record.GetByte(nameof(ScheduledWagonsRecord.DutyOperationDaysFlags)),
            TrainOperationDaysFlags = record.GetByte(nameof(ScheduledWagonsRecord.TrainOperationDaysFlags)),
            OperationDaysFlags = record.GetByte(nameof(ScheduledWagonsRecord.OperationDaysFlags)),
            TurnusNumber = record.GetString(nameof(ScheduledWagonsRecord.TurnusNumber)),
            PositionInTrain = record.GetInt(nameof(ScheduledWagonsRecord.PositionInTrain)),
            MaxNumberOfWagons = record.GetInt(nameof(ScheduledWagonsRecord.MaxNumberOfWagons)),
            Description = record.GetString(nameof(ScheduledWagonsRecord.Description)),
        };

    public static ScheduledWagonsDisconnectRecord ToScheduledWagonsDisconnectNote(this IDataRecord record) =>
        new()
        {
            CallId = record.GetInt(nameof(ScheduledWagonsRecord.CallId)),
            DutyOperationDaysFlags = record.GetByte(nameof(ScheduledWagonsRecord.DutyOperationDaysFlags)),
            TrainOperationDaysFlags = record.GetByte(nameof(ScheduledWagonsRecord.TrainOperationDaysFlags)),
            OperationDaysFlags = record.GetByte(nameof(ScheduledWagonsRecord.OperationDaysFlags)),
            TurnusNumber = record.GetString(nameof(ScheduledWagonsRecord.TurnusNumber)),
            PositionInTrain = record.GetInt(nameof(ScheduledWagonsRecord.PositionInTrain)),
            MaxNumberOfWagons = record.GetInt(nameof(ScheduledWagonsRecord.MaxNumberOfWagons)),
            Description = record.GetString(nameof(ScheduledWagonsRecord.Description)),
        };
}
