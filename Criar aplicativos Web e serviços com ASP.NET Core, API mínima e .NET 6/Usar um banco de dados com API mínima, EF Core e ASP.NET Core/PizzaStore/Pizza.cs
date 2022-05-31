using Microsoft.EntityFrameworkCore;

namespace PizzaStore.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    class PizzaDb : DbContext
    {
        public DbSet<Pizza> Pizzas {get; set; }
        public PizzaDb(DbContextOptions options) : base(options){}
    }


}