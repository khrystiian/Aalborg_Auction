using Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Database
{
    public class ProductRepository : ARepository<Product> 
    {
        public ProductRepository(Dbcontext Context) : base(Context)
        {

        }

        public ProductRepository() : base()
        {

        }

        public IEnumerable<Product> GetAllProductsInCategory(int CategoryId)
        {
            return Set.Where(prd => prd.CategoryId == CategoryId);
        }

        public IEnumerable<Product> FindProductsWithPriceMoreThan(double price)
        {
            return Set.Where(prd => prd.Price >= price).ToList();
        }

        public ParallelQuery<Product> GetAllProductsWithName(string name)
        {
            if(name.Contains(' '))
            {
                return null;

            }
            var NameEquals = Set.AsParallel().Where(prd => prd.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            var NameContains = Set.AsParallel().Where(NameFunc(name));
            return NameEquals.Union(NameContains).Union(GetProductsWithDescription(name));
        }

        public Func<Product, bool> DescriptionFunc(string description)
        {
            return pr => pr.Description.IndexOf(description, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public Func<Product, bool> NameFunc(string name)
        {
            return pr => pr.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public ParallelQuery<Product> GetProductsWithDescription(string description)
        {  
            return Set.AsParallel().Where(DescriptionFunc(description.Trim(' ')));
        }
        public IEnumerable<Product> FindProductsWithPriceLessThan(double price)
        {
            return Set.Where(prd => prd.Price <= price).ToList();
        }

        public override Product getByIdWithObjects(int Id)
        {
            return Set.Where(pr => pr.Id == Id)
                   .Include(x => x.Category) 
                   .FirstOrDefault();
        }
    }
}
