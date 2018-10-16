using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [DataContract(IsReference = true)]
    public class Bid
    {

        public Bid()
        {
        }
        public Bid(double price,DateTime time,Account bidOwner,int auctionId)
        {
            Price = price;
            BidTime = time;
            BidOwnerId = bidOwner.Id;
            BidOwner = bidOwner;
            AuctionId = auctionId;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public double Price { get; set; }
        [DataMember]
        public DateTime BidTime { get; set; }
        [DataMember]
        public int BidOwnerId { get; set; }
        [DataMember]
        public Account BidOwner { get; set; }
        [DataMember]
        public int AuctionId { get; set; }
        [DataMember]
        public Auction Auction { get; set; }

        public override string ToString()
        {
            return " Id : " + Id + " Price : " + Price + " BidTime : " + BidTime + " BidOwnerId : " + BidOwnerId + " AuctionId : " + AuctionId;
        }
        public override int GetHashCode()
        {
            int hash = 11;
            hash = hash * 13 + Id.GetHashCode();
            hash = hash * 13 + Price.GetHashCode();
            hash = hash * 13 + BidTime.GetHashCode();
            hash = hash * 13 + BidOwnerId.GetHashCode();
            hash = hash * 13 + AuctionId.GetHashCode();
            return hash;
        }
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            Bid var = obj as Bid;
            if (GetType() != var.GetType())
                return false;
            if (GetHashCode() != var.GetHashCode())
                return false;
            if (this == var)
                return true;

            return Id == var.Id &&
                    Price == var.Price &&
                    BidTime == var.BidTime &&
                    BidOwnerId == var.BidOwnerId &&
                    AuctionId == var.AuctionId;
        }

    }
}
