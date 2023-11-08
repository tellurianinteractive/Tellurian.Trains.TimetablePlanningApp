using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Data.Mappers;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class OdbcCallNoteRecordsServiceTests
{
    const string connectionString = "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=C:\\Users\\Stefan\\OneDrive\\Modelljärnväg\\Träffar\\2023\\2023-11 Värnamo\\Timetable.accdb;Uid=Admin;Pwd=;";

    private ICallNoteRecordsService? _callNoteRecordsService;

    [TestInitialize]
    public void TestInitialize()
    {
        _callNoteRecordsService = new OdbcCallNoteRecordsService(connectionString);
    }

    [TestMethod]
    public async Task ReadLocoConnectRecords()
    {
        var result = await _callNoteRecordsService!.GetLocoConnectRecordsAsync(27);
        Assert.IsNotNull(result);
        foreach (var record in result) { Console.WriteLine(record); }
    }

    [TestMethod]
    public async Task ReadLocoDisconnectRecords()
    {
        var result = await _callNoteRecordsService!.GetLocoDisconnectRecordsAsync(27);
        Assert.IsNotNull(result);
        foreach (var record in result) { Console.WriteLine(record); }
    }

    [TestMethod]
    public async Task ReadLocoExchangeRecords()
    {
        var result = await _callNoteRecordsService!.GetLocoExchangeRecordsAsync(27);
        Assert.IsNotNull(result);
        foreach (var record in result) { Console.WriteLine(record); }
    }
}
