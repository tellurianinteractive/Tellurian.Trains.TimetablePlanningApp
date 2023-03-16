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

    public Task<IEnumerable<LocoConnectEvent>> GetLocoConnectEventsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<LocoConnectEvent> Data(int layoutId) => layoutId switch
        {
            11 => new LocoConnectEvent()
            {
                CallId = 24,
                LocoOperatorSignature = "SJ",
                LocoClass = "Rc6",
                LocoNumber = "",
                TurnusNumber = 2,
                LocoOperatingDaysFlags = 0b01101010,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily
            }.AsEnumerable(),
            12 => new LocoConnectEvent()
            {
                CallId = 24,
                LocoOperatorSignature = "SJ",
                LocoClass = "Rc6",
                LocoNumber = "",
                TurnusNumber = 1,
                LocoOperatingDaysFlags = 0b00010101,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily
            }.AsEnumerable(),
            13 => new LocoConnectEvent()
            {
                CallId = 24,
                LocoOperatorSignature = "SJ",
                LocoClass = "T44",
                LocoNumber = "232",
                TurnusNumber = 2,
                LocoOperatingDaysFlags = 0b00010101,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
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
            21 => new LocoDisconnectEvent()
            {
                CallId = 24,
                LocoOperatorSignature = "SJ",
                LocoClass = "Rc6",
                TurnusNumber = 2,
                LocoOperatingDaysFlags = 0b01101010,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily
            }.AsEnumerable(),
            22 => new LocoDisconnectEvent()
            {
                CallId = 24,
                LocoOperatorSignature = "SJ",
                LocoClass = "Rc6",
                LocoNumber = "",
                TurnusNumber = 1,
                LocoOperatingDaysFlags = 0b00010101,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily
            }.AsEnumerable(),
            23 => new LocoDisconnectEvent()
            {
                CallId = 25,
                LocoOperatorSignature = "SJ",
                LocoClass = "T44",
                LocoNumber = "232",
                TurnusNumber = 3,
                LocoOperatingDaysFlags = 0b00010101,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = 0b00010101,
                DriveToStagingArea = true
            }.AsEnumerable(),
            24 => new LocoDisconnectEvent()
            {
                LocoOperatorSignature = "SJ",
                CallId = 25,
                LocoClass = "T44",
                LocoNumber = "236",
                TurnusNumber = 8,
                LocoOperatingDaysFlags = OperationDayFlags.Daily,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                TurnLoco = true
            }.AsEnumerable(),
            25 => new LocoDisconnectEvent()
            {
                LocoOperatorSignature = "SJ",
                CallId = 25,
                LocoClass = "T44",
                LocoNumber = "236",
                TurnusNumber = 8,
                LocoOperatingDaysFlags = OperationDayFlags.Daily,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                CirculateLoco = true
            }.AsEnumerable(),
            26 => new LocoDisconnectEvent()
            {
                LocoOperatorSignature = "SJ",
                CallId = 25,
                LocoClass = "T44",
                LocoNumber = "236",
                TurnusNumber = 8,
                LocoOperatingDaysFlags = OperationDayFlags.Daily,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                CirculateLoco = true,
                TurnLoco = true,
            }.AsEnumerable(),
            27 => new LocoDisconnectEvent()
            {
                LocoOperatorSignature = "SJ",
                CallId = 25,
                LocoClass = "T44",
                LocoNumber = "236",
                TurnusNumber = 8,
                LocoOperatingDaysFlags = OperationDayFlags.Daily,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                DriveToStagingArea = true,
                TurnLoco = true,
            }.AsEnumerable(),
            28 => new LocoDisconnectEvent()
            {
                LocoOperatorSignature = "SJ",
                CallId = 25,
                LocoClass = "T44",
                LocoNumber = "236",
                TurnusNumber = 8,
                LocoOperatingDaysFlags = OperationDayFlags.Daily,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                DriveToStagingArea = true,
                TurnLoco = true,
                CirculateLoco = true,
            }.AsEnumerable(),
            _ => Enumerable.Empty<LocoDisconnectEvent>(),
        };
    }
    public Task<IEnumerable<LocoExchangeEvent>> GetLocoExchangeEventsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<LocoExchangeEvent> Data(int layoutId) => layoutId switch
        {
            31 => new LocoExchangeEvent()
            {
                CallId = 31,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                LocoOperatingDaysFlags = OperationDayFlags.Daily,
                ReplacingLocoOperatingDaysFlags = OperationDayFlags.Daily,
                LocoClass = "Ma",
                LocoOperatorSignature = "SJ",
                TurnusNumber = 11,
            }.AsEnumerable(),
            _ => Enumerable.Empty<LocoExchangeEvent>(),
        };
    }

    public Task<IEnumerable<ScheduledWagonsConnectEvent>> GetScheduledWagonsConnectEventsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<ScheduledWagonsConnectEvent> Data(int layoutId) => layoutId switch
        {
            41 => new ScheduledWagonsConnectEvent()
            {
                CallId = 1,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                OperationDayFlags = OperationDayFlags.Daily,
                TurnusNumber = 22,
                MaxNumberOfWagons = 2,
                PositionInTrain = 1,
            }.AsEnumerable(),
            42 => new ScheduledWagonsConnectEvent[]
            {
                new ScheduledWagonsConnectEvent()
                {
                    CallId = 2,
                    DutyOperatingDaysFlags = OperationDayFlags.Daily,
                    TrainOperatingDaysFlags = OperationDayFlags.Daily,
                    OperationDayFlags = OperationDayFlags.Daily,
                    TurnusNumber = 22,
                    MaxNumberOfWagons = 4,
                    PositionInTrain = 2,
                },new ScheduledWagonsConnectEvent()
                {
                    CallId = 2,
                    DutyOperatingDaysFlags = OperationDayFlags.Daily,
                    TrainOperatingDaysFlags = OperationDayFlags.Daily,
                    OperationDayFlags = OperationDayFlags.MoWeFr,
                    TurnusNumber = 21,
                    MaxNumberOfWagons = 2,
                    PositionInTrain = 1,
                }
            },
            _ => Enumerable.Empty<ScheduledWagonsConnectEvent>(),
        };
    }

    public Task<IEnumerable<ScheduledWagonsDisconnectEvent>> GetScheduledWagonsDisconnectEventsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<ScheduledWagonsDisconnectEvent> Data(int layoutId) => layoutId switch
        {
            51 => new ScheduledWagonsDisconnectEvent() { 
                CallId= 3,
                DutyOperatingDaysFlags= OperationDayFlags.Daily,
                TrainOperatingDaysFlags= OperationDayFlags.Daily,
                OperationDayFlags= OperationDayFlags.MoWeFr,
                TurnusNumber= 21,
                MaxNumberOfWagons= 2,
                PositionInTrain= 1,
            }.AsEnumerable(),
            _ => Enumerable.Empty<ScheduledWagonsDisconnectEvent>(),
        };
    }

    public Task<IEnumerable<TrainMeetEvent>> GetTrainMeetEventsAsync(int layoutId)
    {

        return Task.FromResult(Data(layoutId));

        static IEnumerable<TrainMeetEvent> Data(int layoutId) => layoutId switch
        {
            71 => new TrainMeetEvent()
            {
                CallId = 1,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                TrainNumber = 124,
                TrainArrivalTime = new TimeSpan(12, 10, 0),
                TrainDepartureTime = new TimeSpan(12, 15, 0),
                MeetingTrainNumber = 4001,
                MeetingTrainOperatorSignature = "SJ",
                MeetingTrainPrefix = "Gt",
                MeetingTrainOperatingDaysFlags = OperationDayFlags.Daily,
                MeetingTrainArrivalTime = new TimeSpan(12, 11, 0),
                MeetingDepartureTime = new TimeSpan(12, 16, 0),
            }.AsEnumerable(),
            72 => new TrainMeetEvent()
            {
                CallId = 1,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                TrainNumber = 123,
                TrainArrivalTime = new TimeSpan(12, 10, 0),
                TrainDepartureTime = new TimeSpan(12, 15, 0),
                MeetingTrainNumber = 4001,
                MeetingTrainOperatorSignature = "SJ",
                MeetingTrainPrefix = "Gt",
                MeetingTrainOperatingDaysFlags = OperationDayFlags.MoWeFr,
                MeetingTrainArrivalTime = new TimeSpan(12, 11, 0),
                MeetingDepartureTime = new TimeSpan(12, 16, 0),
            }.AsEnumerable(),
            _ => Enumerable.Empty<TrainMeetEvent>(),
        }; ;
    }
    public Task<IEnumerable<WaybillWagonsConnectEvent>> GetWaybillWagonsConnectEventsAsync(int layoutId)
    {

        return Task.FromResult(Data(layoutId));

        static IEnumerable<WaybillWagonsConnectEvent> Data(int layoutId) => layoutId switch
        {
            81 => new WaybillWagonsConnectEvent()
            {
                CallId = 1,
                PositionInTrain = 2,
                DestinationFullName = "Göteborg",
                DestinationSignature = "Gbg",
                DestinationForeColor = "#FFFFFF",
                DestinationBackColor = "#009933",
                OriginFullName = "Uddevalla",
                OriginSignature = "Uv",
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                OperationDaysFlag = OperationDayFlags.Daily,
            }.AsEnumerable(),
            82 => new WaybillWagonsConnectEvent[] {
                    new WaybillWagonsConnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationFullName = "Göteborg",
                        DestinationSignature = "Gbg",
                        DestinationForeColor = "#FFFFFF",
                        DestinationBackColor = "#009933",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                    },
                   new WaybillWagonsConnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 1,
                        MaxNumberOfWagons = 6,
                        DestinationFullName = "Ytterby",
                        DestinationSignature = "Yb",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                   },
             },
            83 => new WaybillWagonsConnectEvent[] {
                    new WaybillWagonsConnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationFullName = "Göteborg",
                        DestinationSignature = "Gbg",
                        DestinationForeColor = "#FFFFFF",
                        DestinationBackColor = "#009933",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.MoWeFr,

                    },
                    new WaybillWagonsConnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 1,
                        MaxNumberOfWagons = 6,
                        DestinationFullName = "Ytterby",
                        DestinationSignature = "Yb",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.TuThSa,

                    },
             },

            _ => Enumerable.Empty<WaybillWagonsConnectEvent>(),
        };
    }
    public Task<IEnumerable<WaybillWagonsDisconnectEvent>> GetWaybillWagonsDisconnectEventsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<WaybillWagonsDisconnectEvent> Data(int layoutId) => layoutId switch
        {
            91 => new WaybillWagonsDisconnectEvent()
            {
                CallId = 1,
                PositionInTrain = 2,
                DestinationFullName = "Göteborg",
                DestinationSignature = "Gbg",
                OriginFullName = "Uddevalla",
                OriginSignature = "Uv",
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                OperationDaysFlag = OperationDayFlags.Daily,
            }.AsEnumerable(),
            92 => new WaybillWagonsDisconnectEvent[] {
                    new WaybillWagonsDisconnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationFullName = "Göteborg",
                        DestinationSignature = "Gbg",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                    },
                   new WaybillWagonsDisconnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 1,
                        DestinationFullName = "Ytterby",
                        DestinationSignature = "Yb",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                    },
             },
            93 => new WaybillWagonsDisconnectEvent[] {
                    new WaybillWagonsDisconnectEvent()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationFullName = "Göteborg",
                        DestinationSignature = "Gbg",
                        DestinationForeColor = "#FFFFFF",
                        DestinationBackColor = "#009933",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                    },
                   new WaybillWagonsDisconnectEvent()
                    {
                        CallId = 2,
                        PositionInTrain = 1,
                        DestinationFullName = "Ytterby",
                        DestinationSignature = "Yb",
                        OriginFullName = "Uddevalla",
                        OriginSignature = "Uv",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                    },
             },
            _ => Enumerable.Empty<WaybillWagonsDisconnectEvent>(),
        };
    }

}

