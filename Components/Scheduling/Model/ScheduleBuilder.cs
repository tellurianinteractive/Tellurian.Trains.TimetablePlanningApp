using TimetablePlanning.Models.Common;

namespace TimetablePlanning.Components.Scheduling;

public static class ScheduleBuilder
{
    public static Schedule Bohusbanan => new("Södra Bohusbanan", BohusbananStations, Trains_Uddevalla_Göteborg);

    private static Station[] BohusbananStations => new Station[]
    {
            new ("Uddevalla C", "Uv") { Km = 89, Tracks = new StationTrack[] { new("1"), new("2"), new("3") } },
            new ("Uddevalla Ö", "Uö") { Km = 87, Tracks = new StationTrack[] { new("1")} },
            new ("Grohed", "Gro") { Km = 75, Tracks = new StationTrack[] { new("1") , new("2") } },
            new ("Ljungskile", "Lj") { Km = 67 , Tracks = new StationTrack[] { new("1"), new("2") }},
            new ("Svenshögen", "Svg") { Km = 60 , Tracks = new StationTrack[] { new("1"), new("2") }},
            new ("Stenungsund", "Snu") { Km = 48 , Tracks = new StationTrack[] { new("1b"), new("2"), new StationTrack("3") , new StationTrack("4") {IsScheduled=false} } },
            new ("Stora Höga", "Sth") { Km = 41 , Tracks = new StationTrack[] { new("1"), new("2") }},
            new ("Kode", "Kde") { Km = 33 , Tracks = new StationTrack[] { new("1"), new("2") }},
            new ("Ytterby", "Yb") { Km = 22 , Tracks = new StationTrack[] { new("1"), new("2") }},
            new ("Säve", "Sve") { Km = 15, Tracks = new StationTrack[] { new("1"), new("2") } },
            new ("Göteborg Kville", "Gk") { Km = 4, Tracks = new StationTrack[] {new("1"), new("2") }},
            new ("Olskroken", "Or") { Km = 2 ,Tracks = new StationTrack[] {new("1"), new("2") }},
            new ("Göteborg C", "G") { Km = 0,Tracks = new StationTrack[] { new("6"), new("7"), new("8"),new("9"), new("10"), new("11") }}
    };

    private static TrainPattern Uddevalla_Göteborg => new("Uv-G", "red")
    {
        Calls = new[]
        {
            new StationCall(BohusbananStations[0].Tracks[0], CallAction(-10), CallAction(0)),
            new StationCall(BohusbananStations[1].Tracks[0], CallAction(3), CallAction(3)),
            new StationCall(BohusbananStations[2].Tracks[1], CallAction(6),CallAction(7)),
            new StationCall(BohusbananStations[3].Tracks[0], CallAction(5),CallAction(6)),
            new StationCall(BohusbananStations[4].Tracks[1], CallAction(6),CallAction(7)),
            new StationCall(BohusbananStations[5].Tracks[0], CallAction(8),CallAction(9)),
            new StationCall(BohusbananStations[6].Tracks[1], CallAction(5),CallAction(6)),
            new StationCall(BohusbananStations[7].Tracks[0], CallAction(6),CallAction(7)),
            new StationCall(BohusbananStations[8].Tracks[1], CallAction(7),CallAction(8)),
            new StationCall(BohusbananStations[9].Tracks[0], CallAction(5),CallAction(6)),
            new StationCall(BohusbananStations[10].Tracks[0], CallAction(5),CallAction(5)),
            new StationCall(BohusbananStations[11].Tracks[0], CallAction(4),CallAction(4)),
            new StationCall(BohusbananStations[12].Tracks[4], CallAction(5),CallAction(15)),

        }
    };

    private static TrainPattern Göteborg_Uddevalla => new("G-Uv", "red")
    {
        Calls = new[]
        {
            new StationCall(BohusbananStations[12].Tracks[4], CallAction(-10),CallAction(0)),
            new StationCall(BohusbananStations[11].Tracks[0], CallAction(3),CallAction(3)),
            new StationCall(BohusbananStations[10].Tracks[0], CallAction(3),CallAction(3)),
            new StationCall(BohusbananStations[9].Tracks[0], CallAction(5),CallAction(6)),
            new StationCall(BohusbananStations[8].Tracks[0], CallAction(7),CallAction(8)),
            new StationCall(BohusbananStations[7].Tracks[0], CallAction(7),CallAction(8)),
            new StationCall(BohusbananStations[6].Tracks[0], CallAction(6),CallAction(7)),
            new StationCall(BohusbananStations[5].Tracks[0], CallAction(5),CallAction(6)),
            new StationCall(BohusbananStations[4].Tracks[0], CallAction(8),CallAction(9)),
            new StationCall(BohusbananStations[3].Tracks[0], CallAction(6),CallAction(7)),
            new StationCall(BohusbananStations[2].Tracks[0], CallAction(7),CallAction(8)),
            new StationCall(BohusbananStations[1].Tracks[0], CallAction(7), CallAction(8)),
            new StationCall(BohusbananStations[0].Tracks[0], CallAction(4), CallAction(10)),

        }
    };

    private static TrainPattern Göteborg_Stenungsund => Göteborg_Uddevalla.SubSection("G-Snu", "blue", 0, 8);
    private static TrainPattern Stenungsund_Göteborg => Uddevalla_Göteborg.SubSection("Snu-G", "blue", 5);

    private static IEnumerable<Train> Trains_Uddevalla_Göteborg =>
                 Uddevalla_Göteborg.CreateTrains(3721, "06:07", 19, 1)
        .Concat(Uddevalla_Göteborg.CreateTrains("blue",3761, "05:37", 2, 1))
        .Concat(Stenungsund_Göteborg.CreateTrains(3251, "06:54", 1, 1))
        .Concat(Stenungsund_Göteborg.CreateTrains(3251, "08:09", 1, 1))
        .Concat(Stenungsund_Göteborg.CreateTrains(3255, "15:09", 3, 1))
        .Concat(Göteborg_Uddevalla.CreateTrains("blue", 3220, "06:10", 1, 1))
        .Concat(Göteborg_Uddevalla.CreateTrains(3220, "06:40", 19, 1))
        .Concat(Göteborg_Stenungsund.CreateTrains(3250, "07:10", 1, 1))
        .Concat(Göteborg_Stenungsund.CreateTrains(3272, "05:40", 1, 1))
        .Concat(Göteborg_Stenungsund.CreateTrains(3260, "14:10", 3, 1))
        .Concat(Göteborg_Uddevalla.CreateTrains("blue",3270, "17:10", 1, 1));


    private static TimeSpan AsMinutes(this int minutes) => TimeSpan.FromMinutes(minutes);
    private static CallAction CallAction(this int minutes) => new(minutes.AsMinutes());
    public static TimeSpan AsTime(this string time) => TimeSpan.Parse(time);

    public static string Minutes(this CallAction callAction) => callAction.Time.Minutes.ToString("00");

}
