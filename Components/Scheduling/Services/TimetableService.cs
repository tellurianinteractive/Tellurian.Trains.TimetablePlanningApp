namespace TimetablePlanning.Components.Scheduling.Services;

public interface ITimetableService
{
    Task<IEnumerable<Schedule>> GetTimetableStretchesAsync();
}

public class ExampleTimetableService : ITimetableService
{
    public async Task<IEnumerable<Schedule>> GetTimetableStretchesAsync()
    {
        var result = new List<Schedule>
        {
            ScheduleBuilder.Bohusbanan()
        };         
        return await Task.FromResult(result).ConfigureAwait(false);
    }
}
