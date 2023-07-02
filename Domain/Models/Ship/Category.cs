namespace Domain.Models.Ship
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Ship>? Ships { get; set; } = null;
    }
}
