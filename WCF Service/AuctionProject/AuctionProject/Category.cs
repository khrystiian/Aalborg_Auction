using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Core
{

    public class Category { 
       public ICollection<Product> Products { get; set; }
        public Category(string name) 
        {
            Products = new List<Product>();
            Name = name;
        }

        public Category()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format(" Id : {0} , Name : {1} ", Id, Name);
        }
        public override int GetHashCode()
        {
            int hash = 11;
            hash = hash * 13 + Id.GetHashCode();
            hash = hash * 13 + Name.GetHashCode();
            return hash;
        }
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            Category var = obj as Category;
            if (GetType() != var.GetType())
                return false;
            if (GetHashCode() != var.GetHashCode())
                return false;
            if (this == var)
                return true;

            return Id == var.Id &&
                    Name == var.Name;
        }
    
    }
}
