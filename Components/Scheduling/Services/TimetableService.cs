namespace TimetablePlanning.Components.Scheduling.Services;

public interface ITimetableService
{
    Task<IEnumerable<Schedule>> GetTimetableStretchesAsync();
}

public class ExampleTimetableService : ITimetableService
{
    public async Task<IEnumerable<Schedule>> GetTimetableStretchesAsync()
    {
        return await Task.FromResult(new[] { ScheduleBuilder.Bohusbanan }).ConfigureAwait(false);
    }
}
