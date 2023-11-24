namespace banking.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Guid AccountId { get; set; }
        public Account account { get; set; }
        
        
    }
}
