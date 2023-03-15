using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetablePlanning.Models.CallNotes.Data;
using TimetablePlanning.Models.CallNotes.Extensions;
using TimetablePlanning.Models.CallNotes.Services;

namespace TimetablePlanning.Models.CallNotes.Tests;
[TestClass]
public class ScheduledWagonsDisconnectNoteTests {

    private CallNotesService? CallNotesService;

    [TestInitialize]
    public void TestInitialize()
    {
        TestHelpers.SetTestLanguage();
        CallNotesService = new CallNotesService(new TestCallEventsService());
    }

    private async Task<IEnumerable<TrainCallNote>> Notes(int testCase) =>
        await CallNotesService!.GetCallNotesAsync(testCase).ConfigureAwait(false);



}
