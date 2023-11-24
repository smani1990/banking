namespace banking.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Type { get; set; }

        public double Balance { get; set; }

    }
}
