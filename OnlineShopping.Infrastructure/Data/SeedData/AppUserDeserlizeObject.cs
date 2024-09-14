namespace OnlineShopping.Infrastructure.Data.SeedData
{
    /*
     * This class is used to deserialize the AppUser object
     * for seeding the database
    */
    public class AppUserDeserlizeObject
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
