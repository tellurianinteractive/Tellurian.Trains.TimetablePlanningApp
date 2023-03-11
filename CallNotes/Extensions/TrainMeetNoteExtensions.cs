using System.Collections.Generic;
using System.Linq;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Models.CallNotes.Extensions;

public static class TrainMeetNoteExtensions {

    public static IEnumerable<TrainMeetNote> AsTrainMeetNotes(this IEnumerable<TrainMeetEvent> events) =>
        events.Select(e => e.AsTrainMeetNote());

    public static TrainMeetNote AsTrainMeetNote(this TrainMeetEvent e) =>
        new()
        {
            ForCallId = e.CallId,
            TrainNumber = e.TrainNumber,
            FromTime = e.FromTime,
            ToTime = e.ToTime,
            DutyOperationDays = e.DutyOperatingDaysFlags.AsOperationDays(),
            TrainOperationDays = e.TrainOperatingDaysFlags.AsOperationDays(),
            MeetingTrain = new()
            {
                Number = e.MeetingTrainNumber,
                Prefix = e.MeetingTrainPrefix,
                OperatorSignature = e.MeetingTrainOperatorSignature,
                OperationDays = e.MeetingTrainOperatingDaysFlags.AsOperationDays(),
            },
        };
}
