#nullable disable

namespace TimetablePlanning.App.Server.Data;

    public partial class Region
    {
        public Region()
        {
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public string EnglishName { get; set; }
        public string LocalName { get; set; }
        public string ColourName { get; set; }
        public string Description { get; set; }
        public string ForeColor { get; set; }
        public string BackColor { get; set; }

        public virtual Country Country { get; set; }
    }
