namespace S3Infrastructure.Models
{
    public class ReserveModel
    {
        public string Id { get; set; }
        public string Room { get; set; }
        public string ReserveOfNumber { get; set; }
        public string ReserverId { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        
    }
}
