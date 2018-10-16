using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Database;
using BussinessLayer;
using System.Threading.Tasks;

namespace BussinessLayer 
{
    public class AuctionController : AController<Auction>
    {

        public AuctionRepository AuctionRepository
        {
            get
            {
                return base.Repository as AuctionRepository;
            }
        }

        public AuctionController() : base (new AuctionRepository())
        {
        }

        public override int Add(Auction auction)
        {
            return AuctionRepository.Add(auction);
        }

        public Auction GetAuctionWithProductId(int productId)
        {
            return AuctionRepository.GetAuctionWithProductId(productId);
        }

        public IEnumerable<Auction> GetAllActiveAuctions()
        {
            return AuctionRepository.GetAllActiveAuctions();
        }

        public IEnumerable<Auction> getAllAucionsForProducts(Product[] products)
        {
            List<Auction> auctions = new List<Auction>();
            Parallel.ForEach(products, (item) =>
            {
                lock (auctions)
                {
                    auctions.Add(GetAuctionWithProductId(item.Id));
                }
            });
            //foreach (var item in products)
            //{
            //        auctions.Add(GetAuctionWithProductId(item.Id));
                
            //}
            return auctions;
        }

        public IEnumerable<Auction> GetAllWithObjects()
        {
            return AuctionRepository.GetAllAuctionsWithObjects();
        }

        public bool CheckIfActive(int Id)
        {
           var acc = GetByIdWithObjects(Id);
           return acc.EndTime > DateTime.Now;
        }

        private void CheckCollectionForActiveActions(IEnumerable<Auction> collection)
        {
            Parallel.ForEach(collection, (item) =>
            {
                lock (item)
                {
                    item.Active = CheckIfActive(item.Id);
                }
            });
            //foreach (var item in collection)
            //{
            //    item.Active = CheckIfActive(item.Id);
            //}
        }

        public ICollection<Auction> GetAuctionsWhereSellerId(int sellerId)
        {
            return AuctionRepository.GetAuctionsWhereSellerId(sellerId).ToList();
        }
        public ICollection<Auction> GetAuctionsWhereWinnerId(int winnerId)
        {
            var auctions = AuctionRepository.GetAuctionsWhereWinnerId(winnerId).ToList();
            CheckCollectionForActiveActions(auctions);
            return auctions;
        }

        public IEnumerable<Auction> GetAuctionsWithLessTimeLeft()
        {
            return GetAllActiveAuctions().OrderBy(auc => auc.EndTime > DateTime.Now);
        }

        public IEnumerable<Auction> GetMostRecentAuctions()
        {
            return GetAllActiveAuctions().Where(auc => auc.EndTime < DateTime.Now).OrderBy(auc => auc.EndTime < DateTime.Now);
        }

        public async Task<IEnumerable<Auction>> GetAllAuctionsWithObjectsAsync()
        {
            return await AuctionRepository.GetAllAuctionsWithObjectsAsync();
        }

        public IEnumerable<Auction> GetAllAuctionsForBids(IEnumerable<Bid> bids)
        {
            var auctions = AuctionRepository.GetAllAuctionsForBids(bids);
            CheckCollectionForActiveActions(auctions);
            return auctions;
        }
    }
}
