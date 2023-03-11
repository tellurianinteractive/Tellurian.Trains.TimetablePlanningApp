using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;
using TimetablePlanning.Utilities.Extensions;

namespace TimetablePlanning.Models.CallNotes.Tests;

/// <summary>
/// This service creates test data for the tests in this assembly.
/// The layoutId corresponds to a test case.
/// </summary>
internal class TestCallEventsService : ICallEventsService {

    public Task<IEnumerable<BlockConnectEvent>> GetBlockConnectEventsAsync(int layoutId)
    {

        return Task.FromResult(Data(layoutId));

        static IEnumerable<BlockConnectEvent> Data(int layoutId) => layoutId switch
        {
            1 => new BlockConnectEvent()
            {
                CallId = 1,
                PositionInTrain = 2,
                DestinationFullName = "Göteborg",
                DestinationSignature = "Gbg",
                DestinationForeColor = "#FFFFFF",
                DestinationBackColor = "#009933",
                OriginFullName = "Uddevalla",
                OriginSignature = "Uv",
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily,
            }.AsEnumerable(),
            2 => new BlockConnectEvent[] {
                    new BlockConnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationFullName = "Göteborg",
                        DestinationSignature = "Gbg",
                        DestinationForeColor = "#FFFFFF",
                        DestinationBackColor = "#009933",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDays.Daily,
                        DutyOperatingDaysFlags = OperationDays.Daily,
                    },
                   new BlockConnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 1,
                        MaxNumberOfWagons = 6,
                        DestinationFullName = "Ytterby",
                        DestinationSignature = "Yb",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDays.Daily,
                        DutyOperatingDaysFlags = OperationDays.Daily,
                    },
             },

            _ => Enumerable.Empty<BlockConnectEvent>(),
        };
    }
    public Task<IEnumerable<BlockDisconnectEvent>> GetBlockDisconnectEventsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<BlockDisconnectEvent> Data(int layoutId) => layoutId switch
        {
            1 => new BlockDisconnectEvent()
            {
                CallId = 1,
                PositionInTrain = 2,
                DestinationFullName = "Göteborg",
                DestinationSignature = "Gbg",
                OriginFullName = "Uddevalla",
                OriginSignature = "Uv",
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily,
            }.AsEnumerable(),
            2 => new BlockDisconnectEvent[] {
                    new BlockDisconnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationFullName = "Göteborg",
                        DestinationSignature = "Gbg",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDays.Daily,
                        DutyOperatingDaysFlags = OperationDays.Daily,
                    },
                   new BlockDisconnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 1,
                        DestinationFullName = "Ytterby",
                        DestinationSignature = "Yb",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDays.Daily,
                        DutyOperatingDaysFlags = OperationDays.Daily,
                    },
             },
            3 => new BlockDisconnectEvent[] {
                    new BlockDisconnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationFullName = "Göteborg",
                        DestinationSignature = "Gbg",
                        DestinationForeColor = "#FFFFFF",
                        DestinationBackColor = "#009933",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDays.Daily,
                        DutyOperatingDaysFlags = OperationDays.Daily,
                    },
                   new BlockDisconnectEvent()
                    {
                        CallId = 2,
                        PositionInTrain = 1,
                        DestinationFullName = "Ytterby",
                        DestinationSignature = "Yb",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDays.Daily,
                        DutyOperatingDaysFlags = OperationDays.Daily,
                    },
             },
            _ => Enumerable.Empty<BlockDisconnectEvent>(),
        };
    }
    public Task<IEnumerable<LocoConnectEvent>> GetLocoConnectEventsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<LocoConnectEvent> Data(int layoutId) => layoutId switch
        {
            1 => new LocoConnectEvent()
            {
                CallId = 24,
                LocoOperatorSignature = "SJ",
                LocoClass = "Rc6",
                LocoNumber = "",
                TurnNumber = "2",
                LocoOperatingDaysFlags = 0b01101010,
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily
            }.AsEnumerable(),
            2 => new LocoConnectEvent()
            {
                CallId = 24,
                LocoOperatorSignature = "SJ",
                LocoClass = "Rc6",
                LocoNumber = "",
                TurnNumber = "1",
                LocoOperatingDaysFlags = 0b00010101,
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily
            }.AsEnumerable(),
            3 => new LocoConnectEvent()
            {
                CallId = 24,
                LocoOperatorSignature = "SJ",
                LocoClass = "T44",
                LocoNumber = "232",
                TurnNumber = "3",
                LocoOperatingDaysFlags = 0b00010101,
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = 0b00010101,
                CollectFromStagingArea = true,
            }.AsEnumerable(),
            _ => Enumerable.Empty<LocoConnectEvent>(),
        };
    }
    public Task<IEnumerable<LocoDisconnectEvent>> GetLocoDisconnectEventsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<LocoDisconnectEvent> Data(int layoutId) => layoutId switch
        {
            1 => new LocoDisconnectEvent()
            {
                CallId = 24,
                LocoOperatorSignature = "SJ",
                LocoClass = "Rc6",
                TurnNumber = "2",
                LocoOperatingDaysFlags = 0b01101010,
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily
            }.AsEnumerable(),
            2 => new LocoDisconnectEvent()
            {
                CallId = 24,
                LocoOperatorSignature = "SJ",
                LocoClass = "Rc6",
                LocoNumber = "",
                TurnNumber = "1",
                LocoOperatingDaysFlags = 0b00010101,
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily
            }.AsEnumerable(),
            3 => new LocoDisconnectEvent()
            {
                CallId = 25,
                LocoOperatorSignature = "SJ",
                LocoClass = "T44",
                LocoNumber = "232",
                TurnNumber = "3",
                LocoOperatingDaysFlags = 0b00010101,
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = 0b00010101,
                DriveToStagingArea = true
            }.AsEnumerable(),
            4 => new LocoDisconnectEvent()
            {
                LocoOperatorSignature = "SJ",
                CallId = 25,
                LocoClass = "T44",
                LocoNumber = "236",
                TurnNumber = "8",
                LocoOperatingDaysFlags = OperationDays.Daily,
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily,
                TurnLoco = true
            }.AsEnumerable(),
            5 => new LocoDisconnectEvent()
            {
                LocoOperatorSignature = "SJ",
                CallId = 25,
                LocoClass = "T44",
                LocoNumber = "236",
                TurnNumber = "8",
                LocoOperatingDaysFlags = OperationDays.Daily,
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily,
                CirculateLoco = true
            }.AsEnumerable(),
            6 => new LocoDisconnectEvent()
            {
                LocoOperatorSignature = "SJ",
                CallId = 25,
                LocoClass = "T44",
                LocoNumber = "236",
                TurnNumber = "8",
                LocoOperatingDaysFlags = OperationDays.Daily,
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily,
                CirculateLoco = true,
                TurnLoco = true,
            }.AsEnumerable(),
            7 => new LocoDisconnectEvent()
            {
                LocoOperatorSignature = "SJ",
                CallId = 25,
                LocoClass = "T44",
                LocoNumber = "236",
                TurnNumber = "8",
                LocoOperatingDaysFlags = OperationDays.Daily,
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily,
                DriveToStagingArea = true,
                TurnLoco = true,
            }.AsEnumerable(),
            8 => new LocoDisconnectEvent()
            {
                LocoOperatorSignature = "SJ",
                CallId = 25,
                LocoClass = "T44",
                LocoNumber = "236",
                TurnNumber = "8",
                LocoOperatingDaysFlags = OperationDays.Daily,
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily,
                DriveToStagingArea = true,
                TurnLoco = true,
                CirculateLoco = true,
            }.AsEnumerable(),
            _ => Enumerable.Empty<LocoDisconnectEvent>(),
        };
    }
    public Task<IEnumerable<TrainMeetEvent>> GetTrainMeetEventsAsync(int layoutId) {

        return Task.FromResult(Data(layoutId));

        static IEnumerable<TrainMeetEvent> Data(int layoutId) => layoutId switch
        {
            1 => new TrainMeetEvent()
            {
                CallId = 1,
                TrainNumber = 124,
                MeetingTrainNumber = 4001,
                MeetingTrainOperatorSignature = "SJ",
                MeetingTrainPrefix = "Gt",
                MeetingTrainOperatingDaysFlags = OperationDays.Daily,
                FromTime = new TimeSpan(12,10,0),
                ToTime = new TimeSpan(12, 15, 0),
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily,
            }.AsEnumerable(),
            2 => new TrainMeetEvent()
            {
                CallId = 1,
                TrainNumber = 123,
                MeetingTrainNumber = 4001,
                MeetingTrainOperatorSignature = "SJ",
                MeetingTrainPrefix = "Gt",
                MeetingTrainOperatingDaysFlags = OperationDays.Daily,
                FromTime = new TimeSpan(12, 10, 0),
                ToTime = new TimeSpan(12, 15, 0),
                TrainOperatingDaysFlags = OperationDays.Daily,
                DutyOperatingDaysFlags = OperationDays.Daily,
            }.AsEnumerable(),
            _ => Enumerable.Empty<TrainMeetEvent>(),
        }; ;
    }
}
