namespace banking.Models.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Guid AccountId { get; set; }
        public AccountDTO account { get; set; }

    }
}
