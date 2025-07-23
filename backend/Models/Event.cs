namespace backend.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Organiser { get; set; }
        public string ContactEmail { get; set; }
        public bool IsPublic { get; set; }
        
    }
}
