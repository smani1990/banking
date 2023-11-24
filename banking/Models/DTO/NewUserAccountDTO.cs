namespace banking.Models.DTO
{
    public class NewUserAccountDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public Account? account { get; set; }

    }
}
