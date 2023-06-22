using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Components.Scheduling;

public static class TrainPatternExtenstions
{
    public static Train CreateTrain(this TrainPattern pattern, string number, TimeSpan startTime)
    {
        var result = new Train(number) { Colour = pattern.Colour };
        foreach (var patternCall in pattern.Calls)
        {
            if (result.Calls.Any())
            {
                var previous = result.Calls.Last();
                result.Add(new StationCall(patternCall.Track, new(previous.Departure.Time.Add(patternCall.Arrival.Time)), new(previous.Departure.Time.Add(patternCall.Departure.Time))) { Train = result });
            }
            else
            {
                result.Add(new StationCall(patternCall.Track, new(patternCall.Arrival.Time.Add(startTime)), new(patternCall.Departure.Time.Add(startTime))) { Train = result });
            }
        }
        return result;
    }

    public static IEnumerable<Train> CreateTrains(this TrainPattern pattern, int startTrainNumber, TimeSpan firstTrainstartTime, int numberOfTrains, TimeSpan trainInterval)
    {
        var result = new List<Train>(numberOfTrains);
        for (var i = 0; i < numberOfTrains; i++)
        {
            var trainNumber = (startTrainNumber + i * 2).ToString();
            var startTime = firstTrainstartTime + trainInterval * i;
            result.Add(pattern.CreateTrain(trainNumber, startTime));

        }
        return result;
    }
    public static IEnumerable<Train> CreateTrains(this TrainPattern pattern, int startTrainNumber, string firstTrainStartTime, int numberOfTrains, int trainInterval) =>
        CreateTrains(pattern , startTrainNumber, firstTrainStartTime.AsTime(), numberOfTrains, TimeSpan.FromHours(trainInterval));

    public static IEnumerable<Train> CreateTrains(this TrainPattern pattern, string colour, int startTrainNumber, string firstTrainStartTime, int numberOfTrains, int trainInterval) =>
        CreateTrains(pattern with { Colour = colour}, startTrainNumber, firstTrainStartTime.AsTime(), numberOfTrains, TimeSpan.FromHours(trainInterval));


    public static TrainPattern SubSection(this TrainPattern pattern, string name, int startIndex, int count = 0) =>
        pattern.SubSection(name, pattern.Colour, startIndex, count);
    
    public static TrainPattern SubSection(this TrainPattern pattern, string name, string colour, int startIndex, int count = 0)
    {
        var subPattern = (count <= 0 ? pattern.Calls.Skip(startIndex) : pattern.Calls.Skip(startIndex).Take(count)).ToArray();
        subPattern[0] = new StationCall(subPattern.First().Track, new CallAction(pattern.Calls.First().Arrival.Time), new CallAction(TimeSpan.Zero));
        subPattern[^1] = new StationCall(subPattern.Last().Track, new(subPattern.Last().Arrival.Time), new(pattern.Calls.Last().Departure.Time));
        return new TrainPattern(name, colour) { Calls = subPattern };
    }
}




