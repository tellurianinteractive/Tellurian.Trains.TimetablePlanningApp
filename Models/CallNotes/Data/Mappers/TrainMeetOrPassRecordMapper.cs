using System.Data;
using System.Text;
using TimetablePlanning.Models.CallNotes.Data.Mappers.Extensions;

namespace TimetablePlanning.Models.CallNotes.Data.Mappers;
public static class TrainMeetOrPassRecordMapper
{
    public static TrainMeetsRecord ToTrainMeetsRecord(this IDataRecord record) => (TrainMeetsRecord)record.ToRecordInternal();
    public static TrainPassesRecord ToTrainPassesRecord(this IDataRecord record) => (TrainPassesRecord)record.ToRecordInternal();
    private static TrainMeetOrPassRecord ToRecordInternal(this IDataRecord record) => 
        new  () {
            CallId = record.GetInt(nameof(TrainMeetOrPassRecord.CallId)),
            DutyOperationDaysFlags = record.GetByte(nameof(TrainMeetOrPassRecord.DutyOperationDaysFlags)),
            TrainOperationDaysFlags = record.GetByte(nameof(TrainMeetOrPassRecord.TrainOperationDaysFlags)),
            TrainOperatorSignature =record.GetString(nameof(TrainMeetOrPassRecord.TrainOperatorSignature)),
            TrainNumber = record.GetString(nameof(TrainMeetOrPassRecord.TrainNumber)),
            TrainArrivalTime = record.GetTimespan(nameof(TrainMeetOrPassRecord.TrainArrivalTime)),
            TrainDepartureTime = record.GetTimespan(nameof(TrainMeetOrPassRecord.TrainDepartureTime)),
            TrainDirection = record.GetInt(nameof(TrainMeetOrPassRecord.TrainDirection)),
            OtherTrainOperationDaysFlags = record.GetByte(nameof(TrainMeetOrPassRecord.OtherTrainOperationDaysFlags)),
            OtherTrainOperatorSignature = record.GetString(nameof(TrainMeetOrPassRecord.OtherTrainOperatorSignature)),
            OtherTrainNumber = record.GetString(nameof(TrainMeetOrPassRecord.OtherTrainNumber)),
            OtherTrainArrivalTime = record.GetTimespan(nameof(TrainMeetOrPassRecord.OtherTrainArrivalTime)),
            OtherTrainDepartureTime = record.GetTimespan(nameof(TrainMeetOrPassRecord.OtherTrainDepartureTime)),
            OtherTrainDirection = record.GetInt(nameof(TrainMeetOrPassRecord.OtherTrainDirection)),
        };
}
