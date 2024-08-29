namespace BartendingApplication.Models
{
    public class User // Customers can register on the webpage
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Customer" or "Bartender", Bartender accounts must be initialized in the Users data table
    }
}
