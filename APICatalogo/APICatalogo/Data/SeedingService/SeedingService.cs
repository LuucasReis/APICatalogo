

using APICatalogo.Model;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Data.SeedingService
{
    public class SeedingService : ISeedingService
    {
        private readonly CatalogoDbContext _context;
        public SeedingService(CatalogoDbContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    _context.Database.Migrate();
                }

            }
            //Aplicar migrations
            catch (Exception)
            {
            }
            if(!(_context.Categories.Any() || _context.Products.Any()))
            {
                Category c1 = new ("Bebidas", "\\Images\\bebidas.jpg");
                Category c2 = new ("Lanches", "\\Images\\lanches.jpg");
                Category c3 = new ("Sobremesas", "\\Images\\sobremesas.jpg");

                Product p = new ("Coca-Cola", "Refrigerante de cola 350ml", 5.45, "\\Images\\coca-cola.jpg",50, c1);
                Product p1= new ("Mega Stacker 4.0", "Sanduíche do bk com onion rings", 40.50, "\\Images\\MegaStacker.jfif",10, c2);
                Product p2= new ("Bolo Festa", "Bolo confeitado para festa", 5.45, "\\Images\\BoloFesta.jpg",20, c3);

                _context.Categories.AddRange(c1, c2, c3);
                _context.Products.AddRange(p, p1, p2);
                _context.SaveChanges();
            }

        }
    }
}
