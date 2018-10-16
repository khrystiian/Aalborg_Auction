using Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class AuctionRepository : ARepository<Auction>
    {
        public AuctionRepository(Dbcontext Context) : base(Context)
        {

        }

        public AuctionRepository() : base()
        {

        }


        public Auction GetAuctionWithProductId(int productId)
        {
            return IncludeObjects(Set.Where(auc => auc.Product.Id == productId)).FirstOrDefault();
        }

        public override int Add(Auction auc)
        {
            int i = 0;
            using (var dbContextTransaction = context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                context.Products.Add(auc.Product);
                context.Accounts.Attach(auc.Seller);
                Set.Add(auc);
                i = context.SaveChanges();
                dbContextTransaction.Commit();
            } 
            return i;
        }

        public IEnumerable<Auction> GetAllAuctionsForBids(IEnumerable<Bid> bids)
        {
            HashSet<Auction> set = new HashSet<Auction>();
            object obj = new object();
            Parallel.ForEach(bids, (item) =>
            {
                lock (obj)
                {
                    set.Add(item.Auction);
                }
            });
            return set;
        }

        public IEnumerable<Auction> GetAllAuctionsWithObjects()
        {
             return IncludeObjects(GetAll());
        }


        
        private IEnumerable<Auction> IncludeObjects(IEnumerable<Auction> auctions)
        {
            List<Auction> list = new List<Auction>();
            object obj = new object();
            Parallel.ForEach(auctions, (item) =>
            {
                lock (obj)
                {
                    list.Add(getByIdWithObjects(item.Id));
                }
            });
            return list;
        }

        public IEnumerable<Auction> GetAuctionsWhereSellerId(int sellerId)
        {
            return IncludeObjects(Set.Where(auc => auc.Seller.Id == sellerId));

        }
        public IEnumerable<Auction> GetAuctionsWhereWinnerId(int winnerId)
        {
            return IncludeObjects(Set.Where(auc => auc.Winner.Id == winnerId));
        }

        public IEnumerable<Auction> GetAllActiveAuctions()
        {
            return IncludeObjects(Set.Where(auc => auc.EndTime > DateTime.Now));
        }

        public IEnumerable<Auction> GetAllFinishedAuctions()
        {
            return IncludeObjects(Set.Where(auc => auc.EndTime < DateTime.Now));
        }
        
        public override Auction getByIdWithObjects(int Id)
        {
            var auction = Set.Where(auc => auc.Id == Id)
                     .Include(x => x.Product)
                     .Include(x => x.Seller)
                     .Include(x => x.Winner)
                     .Include(x => x.Bids).FirstOrDefault();

            auction.Active = auction.EndTime > DateTime.Now;
            return auction;
        }

        #region Async methods


        public async Task<Auction> getByIdWithObjectsAsync(int Id)
        {
           return await Set.Where(auc => auc.Id == Id)
                     .Include(x => x.Product)
                     .Include(x => x.Seller)
                     .Include(x => x.Winner)
                     .Include(x => x.Bids).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Auction>> GetAllAuctionsWithObjectsAsync()
        {
            return await IncludeObjectsAsync(GetAll());
        }
        
        private async Task<IEnumerable<Auction>> IncludeObjectsAsync(IEnumerable<Auction> auctions)
        {
            List<Auction> list = new List<Auction>();
            foreach (var item in auctions)
            {
                var auc = await getByIdWithObjectsAsync(item.Id);
                list.Add(auc);
            }
            return list;
        }

        #endregion
    }

}
