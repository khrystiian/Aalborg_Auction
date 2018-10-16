using Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using System.Text.RegularExpressions;

namespace BussinessLayer
{
    public class ProductControler : AController<Product> 
    {
        public ProductRepository ProductRepository
        {
            get
            {
                return base.Repository as ProductRepository;
            }
        }

        public ProductControler() : base(new ProductRepository())
        {
            
        }
        public IEnumerable<Product> FindProductsWithPriceMoreThan(double price)
        {
           return ProductRepository.FindProductsWithPriceMoreThan(price);
        }

        public IEnumerable<Product> GetAllProductsInCategory(int CategoryId) {
            return ProductRepository.GetAllProductsInCategory(CategoryId);
        }

        public IEnumerable<Product> GetProductsWithName(string name)
        {
            char SplitToken = ' ';
            name = name.TrimEnd(SplitToken).TrimStart(SplitToken);
            name = Regex.Replace(name, @"\s+", " ");
            if (name.Contains(SplitToken))
            {
                string[] arr = name.Split(SplitToken);
                var Set = new HashSet<Product>();
                Parallel.ForEach(arr, (s) =>
                {
                    lock (Set)
                    {
                        Set.UnionWith(ProductRepository.GetAllProductsWithName(s));
                    }
                });
                //foreach (string s in arr)
                //{
                //    Set.UnionWith(ProductRepository.GetAllProductsWithName(s));
                //}
                Func<Product, bool> any = ProductRepository.DescriptionFunc(name);
                Set.UnionWith(Set.Where(any));
                any = ProductRepository.NameFunc(name);
                Set.UnionWith(Set.Where(any));
                return Set;
            }
            return ProductRepository.GetAllProductsWithName(name);
        }

        public IEnumerable<Product> GetProductsWithDescription(string description)
        {
            return ProductRepository.GetProductsWithDescription(description);
        }
        public IEnumerable<Product> FindProductsWithPriceLessThan(double price)
        {
            return ProductRepository.FindProductsWithPriceLessThan(price);
        }

    }
}
