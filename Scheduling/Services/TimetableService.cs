using System.Collections.Generic;
using System.Threading.Tasks;
using Tellurian.Trains.Scheduling.Model;

namespace Tellurian.Trains.Scheduling.Services
{
    public interface ITimetableService
    {
        Task<IEnumerable<TimetableStretch>> GetTimetableStretchesAsync();
    }

    public class ExampleTimetableService : ITimetableService
    {
        public async Task<IEnumerable<TimetableStretch>> GetTimetableStretchesAsync()
        {
            return await Task.FromResult(new[] { TimetableBuilder.Bohusbanan with { TimeAxisDirection = TimeAxisDirection.Vertical } }).ConfigureAwait(false);
        }
    }
}
