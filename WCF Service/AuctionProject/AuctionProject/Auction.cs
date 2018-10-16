using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Core
{
    public class Auction
    {
       
        public Auction()
        {
            Bids = new List<Bid>();
        }

        public Auction(Product pr, DateTime start, DateTime end, Account seller)
        {
            Bids = new List<Bid>();
            Product = pr;
            StartTime = start;
            EndTime = end;
            Seller = seller;
        }

        public List<Bid> Bids { get; set; }
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Account Seller { get; set; }
        public Account Winner { get; set; }
        public string CurrentOwnerName { get; set; }
        public double CurrentHighestBid { get; set; }
        [NotMapped]
        public bool Active { get; set; }



        public override string ToString()
        {
            return " Id : " + Id + " StartTime : " + StartTime + " EndTime : " + EndTime +  " CurrentOwnerName : " + CurrentOwnerName + " CurrentHighestBid : " + CurrentHighestBid;
        }
        public override int GetHashCode()
        {
            int hash = 11;
            hash = hash * 13 + Id.GetHashCode();
            hash = hash * 13 + StartTime.GetHashCode();
            hash = hash * 13 + EndTime.GetHashCode();
            hash = hash * 13 + CurrentOwnerName.GetHashCode();
            hash = hash * 13 + CurrentHighestBid.GetHashCode();
            return hash;
        }
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            Auction var = obj as Auction;
            if (GetType() != var.GetType())
                return false;
            if (GetHashCode() != var.GetHashCode())
                return false;
            if (this == var)
                return true;

            return Id == var.Id &&
                    StartTime == var.StartTime &&
                    EndTime == var.EndTime &&
                    CurrentOwnerName == var.CurrentOwnerName &&
                    CurrentHighestBid == var.CurrentHighestBid;
        }
    }

}
