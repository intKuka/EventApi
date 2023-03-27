namespace PaymentService
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public PayState State { get; set; } = PayState.Hold; 
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;
        public DateTime? DateConfirmation { get; set; }
        public DateTime? DateCancellation { get; set; }
        public  string Description { get; set; } = string.Empty;
    }

    public enum PayState
    {
        Hold,
        Confirmed,
        Canceled,
    }
}
