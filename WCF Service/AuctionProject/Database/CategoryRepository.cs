using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Database
{
    public class CategoryRepository : ARepository<Category>
    {
        public CategoryRepository(Dbcontext context):base(context)
        {
            
        }

        public CategoryRepository() : base()
        {

        }

        public override Category getByIdWithObjects(int Id)
        {
            return Set.Where(cat => cat.Id == Id)
                   .Include(x => x.Products)
                   .FirstOrDefault();
        }

        public Category GetCategoryByName(string name)
        {
            return Set.FirstOrDefault(cat => cat.Name.Equals(name));
        }

        public override int Update(Category entity)
        {
            Set.AddOrUpdate(entity);
            return 1; 
        }

        public int RemoveCategoryByName(string name)
        {
            return RemoveById(GetCategoryByName(name).Id);
        }

    }
}
