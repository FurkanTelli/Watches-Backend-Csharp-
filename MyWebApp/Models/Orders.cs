namespace MyWebApp.Models
{
    public class Orders
    {
        public Guid OrderId { get; set; }
        public string? UserId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Quantity
        {
            get; set;
        }
        public string? WatchName { get; set; }
        public string? WatchBrand { get; set; }
    }
}
