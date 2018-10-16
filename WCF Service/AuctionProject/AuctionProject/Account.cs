using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;


namespace Core
{
    public class Account  {

        public Account() { }
        public Account(string email, string password, string salt)
        {
            Email = email;
            Password = password;
            Salt = salt;
            UserName = email;
        }
        public Account(string email, string password)
        {
            Email = email;
            Password = password;
            UserName = email;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
       
        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public double? Balance { get; set; }
        public string  Salt  { get; set; }


        public override string ToString()
        {
            return " Id : " + Id + " UserName : " + UserName + " Password : " + Password + " Email : " + Email + " Fname : " + Fname + " Lname : " + Lname + " Addres : " + Address + " PhoneNumber : " + PhoneNumber + " Balance : " + Balance + " Salt : " + Salt;
        }
        public override int GetHashCode()
        {
            int hash = 11;
            hash = hash * 13 + Id.GetHashCode();
            hash = hash * 13 + UserName.GetHashCode();
            hash = hash * 13 + Password.GetHashCode();
            hash = hash * 13 + Email.GetHashCode();
            hash = hash * 13 + Fname.GetHashCode();
            hash = hash * 13 + Lname.GetHashCode();
            hash = hash * 13 + Address.GetHashCode();
            hash = hash * 13 + PhoneNumber.GetHashCode();
            hash = hash * 13 + Balance.GetHashCode();
            hash = hash * 13 + Salt.GetHashCode();
            return hash;
        }
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            Account var = obj as Account;
            if (GetType() != var.GetType())
                return false;
            if (GetHashCode() != var.GetHashCode())
                return false;
            if (this == var)
                return true;

            return Id == var.Id &&
                    UserName == var.UserName &&
                    Password == var.Password &&
                    Email == var.Email &&
                    Fname == var.Fname &&
                    Lname == var.Lname &&
                    Address == var.Address &&
                    PhoneNumber == var.PhoneNumber &&
                    Balance == var.Balance &&
                    Salt == var.Salt;
        }
    }
}
