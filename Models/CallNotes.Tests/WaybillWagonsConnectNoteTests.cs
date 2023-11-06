using Microsoft.AspNetCore.Components;
using TimetablePlanning.Models.CallNotes.Services;

namespace TimetablePlanning.Models.CallNotes.Tests;

[TestClass]
public class WaybillWagonsConnectNoteTests {

    private CallNotesService? CallNotesService;

    [TestInitialize]
    public void TestInitialize()
    {
        TestHelpers.SetTestLanguage();
        CallNotesService = new CallNotesService(new TestCallEventsService());
    }

    private async Task<IEnumerable<TrainCallNote>> Notes(int testCase) =>
        await CallNotesService!.GetCallNotesAsync(testCase).ConfigureAwait(false);



    [TestMethod]
    public async Task SingleNote()
    {
        var notes = await Notes(81);
        var note = notes.First();

        const string expected = """

            <div class="callnote text">Connect wagons to </div>
            <div class="callnote item"><span class="callnote destination region" style="background-color: #009933; color: #FFFFFF">Göteborg</span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task SeveralDestinations()
    {
        var notes = await Notes(82);
        var note = notes.First();

        const string expected = """

            <div class="callnote text">Connect wagons to </div>
            <div class="callnote item"><span class="callnote destination other">Ytterby</span></div>
            <div class="callnote item"><span class="callnote destination region" style="background-color: #009933; color: #FFFFFF">Göteborg</span></div>
            """;

        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    [TestMethod]
    public async Task DestinationsDifferentDays()
    {
        var notes = await Notes(83);
        var note = notes.First();

        const string expected = """
            
            <div class="callnote text">Connect wagons to </div>
            <div class="callnote item"><span class="callnote days">Tu,Th,Sa: </span><span class="callnote destination other">Ytterby</span></div>
            <div class="callnote item"><span class="callnote days">Mo,We,Fr: </span><span class="callnote destination region" style="background-color: #009933; color: #FFFFFF">Göteborg</span></div>
            """;
        Assert.AreEqual(new MarkupString(expected), note.Markup());
    }

    private void Compare(string one, string another)
    {
        //Assert.AreEqual(one.Length, another.Length, "Differts in length");
        for (int i = 0; i < one.Length; i++)
        {
            Assert.AreEqual(one[i], another[i], $"{one[i]} differts from {another[i]} at position {i}");
        }
    }
}
