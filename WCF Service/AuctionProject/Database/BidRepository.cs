using Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class BidRepository : ARepository<Bid>
    { 
        public BidRepository(Dbcontext Context) : base(Context)
        {
         
        }

        public BidRepository() : base()
        {

        }


        private bool CheckValidBidPrice(Bid latest, Bid bid)
        {
            return latest.Price < bid.Price;
        }

        private bool CheckValidBidPrice(Bid bid, Auction auction)
        {
            return bid.Price > auction.Product.Price;
        }

        private void AssignVariables(Bid bid, Auction auction)
        {
            auction.CurrentHighestBid = bid.Price;
            auction.CurrentOwnerName = bid.BidOwner.UserName;
            bid.Auction = auction;
            bid.BidOwner = null;
        }

        public override int Add(Bid bid)
        {
            int i = 0;
            using (var dbContextTransaction = context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                int auctionId = bid.AuctionId;
                var latest = GetLatestBidForAucion(auctionId);
                bool condition;
                Auction auction;
                if (latest != null)
                {
                    condition = CheckValidBidPrice(latest, bid);
                    auction = latest.Auction;
                }
                else
                {
                    AuctionRepository repo = new AuctionRepository(context);
                    auction = repo.getByIdWithObjects(auctionId);
                    condition = CheckValidBidPrice(bid, auction);
                }

                if (!condition)
                    return 0;
                else
                    AssignVariables(bid, auction);

                context.Auctions.Attach(bid.Auction);
                context.Entry(bid.Auction).State = EntityState.Modified;
                Set.Add(bid);

                i = context.SaveChanges();
                dbContextTransaction.Commit();                
            }
            return i;
        }

        public Bid GetLatestBidForAucion(int auctionId)
        {
             var bidsForAuction = GetAllBidsByAuctionId(auctionId);
             return bidsForAuction
                    .OrderByDescending(bid => bid.BidTime)
                    .FirstOrDefault();
        }

        public ParallelQuery<Bid> GetAllBidsByAccountId(int accountId)
        {
            return Include(Set).Where(bid => bid.BidOwnerId == accountId);
        }

        private ParallelQuery<Bid> Include(IQueryable<Bid> Set)
        {
            return Set.Include(x => x.BidOwner)
                  .Include(x => x.Auction).AsParallel();
        }

        public Bid GetBidWherePrice(double price, ParallelQuery<Bid> collection)
        {
           return collection.Where(bid => bid.Price == price).FirstOrDefault();
        }

        public Bid GetBidForAuctionWherePrice(int auctionId,double price)
        {
            var bids = GetAllBidsByAuctionId(auctionId);
            return GetBidWherePrice(price, bids);
        }


        public ParallelQuery<Bid> GetAllBidsByAuctionId(int auctionId)
        {
            return Include(Set).Where(bid => bid.AuctionId == auctionId);

        }

        public Bid GetBidWithObjectsWithAuctionId(int auctionId)
        {
            return GetAllBidsByAuctionId(auctionId).FirstOrDefault();
        }

        public Bid GetBidWithObjectsWithAccountId(int accountId)
        {
            return GetAllBidsByAccountId(accountId).FirstOrDefault();
        }
        

        public override Bid getByIdWithObjects(int Id)
        {
            return Set.Where(bid => bid.Id == Id)
             .Include(x => x.BidOwner)
             .Include(x => x.Auction)
             .FirstOrDefault();          
        }
    }
}
