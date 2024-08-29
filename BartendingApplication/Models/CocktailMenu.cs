// To add or remove items from the menu, access the associated table
namespace BartendingApplication.Models
{
    public class CocktailMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
