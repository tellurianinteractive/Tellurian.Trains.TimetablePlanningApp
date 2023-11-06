using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.Common;
using TimetablePlanning.Utilities.Extensions;

namespace TimetablePlanning.Models.CallNotes.Tests;

/// <summary>
/// This service creates test data for the tests in this assembly.
/// The layoutId corresponds to a test case.
/// </summary>
internal class TestCallEventsService : ICallNoteRecordsService
{

    public Task<IEnumerable<LocoConnectRecord>> GetLocoConnectRecordsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<LocoConnectRecord> Data(int layoutId) => layoutId switch
        {
            11 => new LocoConnectRecord()
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
            12 => new LocoConnectRecord()
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
            13 => new LocoConnectRecord()
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
            _ => Enumerable.Empty<LocoConnectRecord>(),
        };
    }
    public Task<IEnumerable<LocoDisconnectRecord>> GetLocoDisconnectRecordsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<LocoDisconnectRecord> Data(int layoutId) => layoutId switch
        {
            21 => new LocoDisconnectRecord()
            {
                CallId = 24,
                LocoOperatorSignature = "SJ",
                LocoClass = "Rc6",
                TurnusNumber = 2,
                LocoOperatingDaysFlags = 0b01101010,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily
            }.AsEnumerable(),
            22 => new LocoDisconnectRecord()
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
            23 => new LocoDisconnectRecord()
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
            24 => new LocoDisconnectRecord()
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
            25 => new LocoDisconnectRecord()
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
            26 => new LocoDisconnectRecord()
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
            27 => new LocoDisconnectRecord()
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
            28 => new LocoDisconnectRecord()
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
            _ => Enumerable.Empty<LocoDisconnectRecord>(),
        };
    }
    public Task<IEnumerable<LocoExchangeRecord>> GetLocoExchangeRecordsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<LocoExchangeRecord> Data(int layoutId) => layoutId switch
        {
            31 => new LocoExchangeRecord()
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
            _ => Enumerable.Empty<LocoExchangeRecord>(),
        };
    }

    public Task<IEnumerable<ScheduledWagonsConnectRecord>> GetScheduledWagonsConnectRecordsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<ScheduledWagonsConnectRecord> Data(int layoutId) => layoutId switch
        {
            41 => new ScheduledWagonsConnectRecord()
            {
                CallId = 1,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                OperationDayFlags = OperationDayFlags.Daily,
                TurnusNumber = 22,
                MaxNumberOfWagons = 2,
                PositionInTrain = 1,
            }.AsEnumerable(),
            42 => new ScheduledWagonsConnectRecord[]
            {
                new()
                {
                    CallId = 2,
                    DutyOperatingDaysFlags = OperationDayFlags.Daily,
                    TrainOperatingDaysFlags = OperationDayFlags.Daily,
                    OperationDayFlags = OperationDayFlags.Daily,
                    TurnusNumber = 22,
                    MaxNumberOfWagons = 4,
                    PositionInTrain = 2,
                },new()
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
            _ => Enumerable.Empty<ScheduledWagonsConnectRecord>(),
        };
    }

    public Task<IEnumerable<ScheduledWagonsDisconnectRecord>> GetScheduledWagonsDisconnectRecordsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<ScheduledWagonsDisconnectRecord> Data(int layoutId) => layoutId switch
        {
            51 => new ScheduledWagonsDisconnectRecord()
            {
                CallId = 3,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                OperationDayFlags = OperationDayFlags.MoWeFr,
                TurnusNumber = 21,
                MaxNumberOfWagons = 2,
                PositionInTrain = 1,
            }.AsEnumerable(),
            _ => Enumerable.Empty<ScheduledWagonsDisconnectRecord>(),
        };
    }

    public Task<IEnumerable<TrainMeetRecord>> GetTrainMeetRecordsAsync(int layoutId)
    {

        return Task.FromResult(Data(layoutId));

        static IEnumerable<TrainMeetRecord> Data(int layoutId) => layoutId switch
        {
            71 => new TrainMeetRecord()
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
            72 => new TrainMeetRecord()
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
            _ => Enumerable.Empty<TrainMeetRecord>(),
        }; ;
    }
    public Task<IEnumerable<WagonGroupConnectRecord>> GetWagonGroupsConnectRecordsAsync(int layoutId)
    {

        return Task.FromResult(Data(layoutId));

        static IEnumerable<WagonGroupConnectRecord> Data(int layoutId) => layoutId switch
        {
            81 => new WagonGroupConnectRecord()
            {
                CallId = 1,
                PositionInTrain = 2,
                DestinationName = "Göteborg",
                DestinationBackColor = "#009933",
                OriginStationName = "Uddevalla",
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                OperationDaysFlag = OperationDayFlags.Daily,
            }.AsEnumerable(),
            82 => new WagonGroupConnectRecord[] {
                    new()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationName = "Göteborg",
                        DestinationBackColor = "#009933",
                        OriginStationName = "Uddevalla",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                    },
                   new()
                    {
                        CallId = 1,
                        PositionInTrain = 1,
                        MaxNumberOfWagons = 6,
                        DestinationName = "Ytterby",
                        OriginStationName = "Uddevalla",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                   },
             },
            83 => new WagonGroupConnectRecord[] {
                    new()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationName = "Göteborg",
                        DestinationBackColor = "#009933",
                        OriginStationName = "Uddevalla",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.MoWeFr,

                    },
                    new()
                    {
                        CallId = 1,
                        PositionInTrain = 1,
                        MaxNumberOfWagons = 6,
                        DestinationName = "Ytterby",
                        OriginStationName = "Uddevalla",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.TuThSa,

                    },
             },

            _ => Enumerable.Empty<WagonGroupConnectRecord>(),
        };
    }
    public Task<IEnumerable<WagonGroupDisconnectRecord>> GetWagonGroupsDisconnectRecordsAsync(int layoutId)
    {
        return Task.FromResult(Data(layoutId));

        static IEnumerable<WagonGroupDisconnectRecord> Data(int layoutId) => layoutId switch
        {
            91 => new WagonGroupDisconnectRecord()
            {
                CallId = 1,
                PositionInTrain = 2,
                DestinationName = "Göteborg",
                OriginStationName = "Uddevalla",
                TrainOperatingDaysFlags = OperationDayFlags.Daily,
                DutyOperatingDaysFlags = OperationDayFlags.Daily,
                OperationDaysFlag = OperationDayFlags.Daily,
            }.AsEnumerable(),
            92 => new WagonGroupDisconnectRecord[] {
                    new()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationName = "Göteborg",
                        DestinationBackColor = "#009933",
                        OriginStationName = "Uddevalla",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                    },
                   new()
                    {
                        CallId = 1,
                        PositionInTrain = 1,
                        DestinationName = "Ytterby",
                        OriginStationName = "Uddevalla",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                    },
             },
            93 => new WagonGroupDisconnectRecord[] {
                    new()
                    {
                        CallId = 1,
                        PositionInTrain = 2,
                        DestinationName = "Göteborg",
                        DestinationBackColor = "#009933",
                        OriginStationName = "Uddevalla",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                    },
                   new()
                    {
                        CallId = 2,
                        PositionInTrain = 1,
                        DestinationName = "Ytterby",
                        OriginStationName = "Uddevalla",
                        TrainOperatingDaysFlags = OperationDayFlags.Daily,
                        DutyOperatingDaysFlags = OperationDayFlags.Daily,
                        OperationDaysFlag = OperationDayFlags.Daily,
                    },
             },
            _ => Enumerable.Empty<WagonGroupDisconnectRecord>(),
        };
    }

}

