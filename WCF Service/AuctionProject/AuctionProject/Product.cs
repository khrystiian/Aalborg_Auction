using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Core
{
    public class Product
    {
        public Product()
        {       
        }
        public Product(string name,string description,double price,int categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string Picture { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public override string ToString()
        {
            return " Id : " + Id + " Name : " + Name + " Description : " + Description + " Price : " + Price + " Picture : " + Picture + " CategoryId : " + CategoryId;
        }
        public override int GetHashCode()
        {
            int hash = 11;
            hash = hash * 13 + Id.GetHashCode();
            hash = hash * 13 + Name.GetHashCode();
            hash = hash * 13 + Description.GetHashCode();
            hash = hash * 13 + Price.GetHashCode();
         //   hash = hash * 13 + Picture.GetHashCode();
            hash = hash * 13 + CategoryId.GetHashCode();
            return hash;
        }
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            Product var = obj as Product;
            if (GetType() != var.GetType())
                return false;
            if (GetHashCode() != var.GetHashCode())
                return false;
            if (this == var)
                return true;

            return Id == var.Id &&
                    Name == var.Name &&
                    Description == var.Description &&
                    Price == var.Price &&
                //    Picture == var.Picture &&
                    CategoryId == var.CategoryId;
        }
    }
}
