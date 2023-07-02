namespace Persistence.DataSeed
{
    using Domain.Models.Ship;

    public static class DataSeed
    {
        public static List<Category> AddDefaultCategories()
        {
            return new List<Category>{
                new Category{Id = 1, Name = "Ocean Cruise Ship", Ships = new List<Ship>()},
                new Category{Id = 2, Name = "Luxury Cruise Ship", Ships = new List<Ship>()},
                new Category{Id = 3, Name = "Small Cruise Ship", Ships = new List<Ship>()},
                new Category{Id = 4, Name = "Adventure Cruise Ship", Ships = new List<Ship>()},
                new Category{Id = 5, Name = "Expedition Cruise Ship", Ships = new List<Ship>()},
                new Category{Id = 6, Name = "River Cruise Ship", Ships = new List<Ship>()},
                new Category{Id = 7, Name = "Mega Cruise Ship", Ships = new List<Ship>()},
                new Category{Id = 8, Name = "Mainstream Cruise Ship", Ships = new List<Ship>()},
            };
        }
    }
}
