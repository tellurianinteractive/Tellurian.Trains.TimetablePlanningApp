using System.Data;
using System.Text;
using TimetablePlanning.Models.CallNotes.Data.Mappers.Extensions;

namespace TimetablePlanning.Models.CallNotes.Data.Mappers;
public static class TrainMeetRecordMapper
{
    public static TrainMeetRecord ToTrainMeetRecord(this IDataRecord record) =>
        new() {
            CallId = record.GetInt(nameof(TrainMeetRecord.CallId)),
            DutyOperationDaysFlags = record.GetByte(nameof(TrainMeetRecord.DutyOperationDaysFlags)),
            TrainOperationDaysFlags = record.GetByte(nameof(TrainMeetRecord.TrainOperationDaysFlags)),
            TrainNumber = record.GetString(nameof(TrainMeetRecord.TrainNumber)),
            TrainArrivalTime = record.GetTimespan(nameof(TrainMeetRecord.TrainArrivalTime)),
            TrainDepartureTime = record.GetTimespan(nameof(TrainMeetRecord.TrainDepartureTime)),
            MeetingTrainOperationDaysFlags = record.GetByte(nameof(TrainMeetRecord.MeetingTrainOperationDaysFlags)),
            MeetingTrainOperatorSignature = record.GetString(nameof(TrainMeetRecord.MeetingTrainOperatorSignature)),
            MeetingTrainNumber = record.GetString(nameof(TrainMeetRecord.MeetingTrainNumber)),
            MeetingTrainArrivalTime = record.GetTimespan(nameof(TrainMeetRecord.MeetingTrainArrivalTime)),
            MeetingTrainDepartureTime = record.GetTimespan(nameof(TrainMeetRecord.MeetingTrainDepartureTime)),
        };
}
