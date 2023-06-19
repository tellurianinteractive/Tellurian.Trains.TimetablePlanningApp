#nullable disable

namespace TimetablePlanning.App.Server.Data;

public class User
    {
        public int Id { get; set; }
        public Guid ObjectId { get; set; }
        public virtual Person Person { get; set; }

    }
