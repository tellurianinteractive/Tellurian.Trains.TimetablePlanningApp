#nullable disable

namespace TimetablePlanning.App.Server.Data;

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int CountryId { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Country Country { get; set; }
    }
