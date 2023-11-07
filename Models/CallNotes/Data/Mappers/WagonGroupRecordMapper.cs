using System.Data;
using TimetablePlanning.Models.CallNotes.Data.Mappers.Extensions;

namespace TimetablePlanning.Models.CallNotes.Data.Mappers;
public static class WagonGroupRecordMapper
{
    public static WagonGroupConnectRecord ToWagonGroupConnectRecord(this IDataRecord record) =>
        new()
        {
            CallId = record.GetInt(nameof(LocoRecord.CallId)),
            DutyOperationDaysFlags = record.GetByte(nameof(WagonRecord.DutyOperationDaysFlags)),
            TrainOperationDaysFlags = record.GetByte(nameof(WagonRecord.TrainOperationDaysFlags)),
            OperationDaysFlags = record.GetByte(nameof(WagonRecord.OperationDaysFlags)),
            DestinationName = record.GetString(nameof(WagonRecord.DestinationName)),
            OriginStationName = record.GetString(nameof(WagonRecord.OriginStationName)),
            DestinationBackColor = record.GetString(nameof(WagonGroupRecord.DestinationBackColor)),
            CountryDomain = record.GetString(nameof(WagonGroupRecord.CountryDomain)),
            FlagHref = record.GetString(nameof(WagonGroupRecord.FlagHref)),
            DestinationAndBeyond = record.GetBool(nameof(WagonGroupRecord.DestinationAndBeyond)),
            OriginAndBefore = record.GetBool(nameof(WagonGroupRecord.OriginAndBefore)),
            DisplayOrder = record.GetInt(nameof(WagonGroupRecord.DisplayOrder)),
            PositionInTrain = record.GetInt(nameof(WagonGroupRecord.PositionInTrain)),
            MaxNumberOfWagons = record.GetInt(nameof(WagonGroupRecord.MaxNumberOfWagons)),
            ToAllDestinations = record.GetBool(nameof(WagonGroupRecord.ToAllDestinations)),
            TransferDestinationName = record.GetString(nameof(WagonGroupRecord.TransferDestinationName))
        };
    public static WagonGroupDisconnectRecord ToWagonGroupDisconnectRecord(this IDataRecord record) =>
        new()
        {
            CallId = record.GetInt(nameof(LocoRecord.CallId)),
            DutyOperationDaysFlags = record.GetByte(nameof(WagonRecord.DutyOperationDaysFlags)),
            TrainOperationDaysFlags = record.GetByte(nameof(WagonRecord.TrainOperationDaysFlags)),
            OperationDaysFlags = record.GetByte(nameof(WagonRecord.OperationDaysFlags)),
            DestinationName = record.GetString(nameof(WagonRecord.DestinationName)),
            OriginStationName = record.GetString(nameof(WagonRecord.OriginStationName)),
            DestinationBackColor = record.GetString(nameof(WagonGroupRecord.DestinationBackColor)),
            CountryDomain = record.GetString(nameof(WagonGroupRecord.CountryDomain)),
            FlagHref = record.GetString(nameof(WagonGroupRecord.FlagHref)),
            DestinationAndBeyond = record.GetBool(nameof(WagonGroupRecord.DestinationAndBeyond)),
            OriginAndBefore = record.GetBool(nameof(WagonGroupRecord.OriginAndBefore)),
            DisplayOrder = record.GetInt(nameof(WagonGroupRecord.DisplayOrder)),
            PositionInTrain = record.GetInt(nameof(WagonGroupRecord.PositionInTrain)),
            MaxNumberOfWagons = record.GetInt(nameof(WagonGroupRecord.MaxNumberOfWagons)),
            ToAllDestinations = record.GetBool(nameof(WagonGroupRecord.ToAllDestinations)),
            TransferDestinationName = record.GetString(nameof(WagonGroupRecord.TransferDestinationName))
        };

}
