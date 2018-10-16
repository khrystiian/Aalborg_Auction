using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using System.Data.Entity;
using Core;

namespace BussinessLayer
{
    public class BidController : AController<Bid>
    {
        public BidRepository BidRepository
        {
            get
            {
                return base.Repository as BidRepository;
            }
        }
        
        public BidController() : base(new BidRepository())
        {

        }

        public double GetBidWithHighestPrice(Auction auction)
        {
            var bids = GetAllBidsByAuctionId(auction.Id);
            return bids.Max(bid => bid.Price);
        }

        public override int Add(Bid entity)
        {
            return BidRepository.Add(entity);
        }

        public Bid GetBidForAuctionWherePrice(double price,Auction auction)
        {
           return BidRepository.GetBidForAuctionWherePrice(auction.Id,price);
        }

        public Bid getByIdWithObjects(int id)
        {
           return BidRepository.getByIdWithObjects(id);
        }

        public Bid GetBidWithObjectsWithAuctionId(int auctionId)
        {
            AuctionController aucCtrl = new AuctionController();
            var auc = aucCtrl.GetByIdWithObjects(auctionId);
            if (auc.Bids.Count > 0)
            {
                var bid = BidRepository.GetBidWithObjectsWithAuctionId(auctionId);
                bid.Auction = auc;
                return bid;
            }
            return null;

        }

        public Bid GetBidWithObjectsWithAccountId(int accountId)
        {
            return BidRepository.GetBidWithObjectsWithAccountId(accountId);
        }


        public Bid CreateBid(double price,Account bidOwner,int auctionId)
        {
            return new Bid(price, DateTime.Now, bidOwner, auctionId);
        }

        public IEnumerable<Bid> GetAllBidsByAccountId(int accountId)
        {
            return BidRepository.GetAllBidsByAccountId(accountId);
        }

        public IEnumerable<Bid> GetAllBidsByAuctionId(int auctionId)
        {
            return BidRepository.GetAllBidsByAuctionId(auctionId);
        }
    }
}
