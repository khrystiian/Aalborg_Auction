using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Core;
namespace WcfServiceLibraryAuction
{
    [ServiceContract]
    interface IAuctionProjectService
    {
        [OperationContract]
        void LogError(Exception e);

        #region AccountService
        [OperationContract]
        Account GetAccountById(int Id);

        [OperationContract]
        Account HashAndSaltPassword(Account acc);

        [OperationContract]
        bool Login(Account acc, string password);
       

        [OperationContract]
        int AddAccount(Account Account);

        [OperationContract]
        ICollection<Account> GetAllAccounts();

        [OperationContract]
        void RemoveAccount(Account Account);

        [OperationContract]
        int RemoveAccountById(int Id);

        [OperationContract]
        int UpdateAccount(Account Account);
        [OperationContract]
        Account GetAccountByUsername(string username);
        [OperationContract]
        Account GetAccountByEmail(string email);
        [OperationContract]
        Account GetAccountWithObjects(int Id);

        #endregion

        #region AuctionService
        [OperationContract]
        Auction GetAuctionById(int Id);

        [OperationContract]
        int AddAuction(Auction Auction);
       
        [OperationContract]
        ICollection<Auction> GetAllAuctions();

        [OperationContract]
        ICollection<Auction> GetAllAuctionsWithObjects();

        [OperationContract]
        IEnumerable<Auction> GetAllActiveAuctions();

        [OperationContract]
        Task<IEnumerable<Auction>> GetAllAuctionsAsyncable();

        [OperationContract]
        void RemoveAuction(Auction Auction);

        [OperationContract]
        int RemoveAuctionById(int Id);

        [OperationContract]
        bool CheckIfActive(int Id);

        [OperationContract]
        int UpdateAuction(Auction Auction);

        [OperationContract]
        Auction GetAuctionWithProductId(int productId);
        [OperationContract]
        IEnumerable<Auction> getAllAucionsForProducts(Product[] products);

        [OperationContract]
        IEnumerable<Auction> GetAuctionsWhereSellerId(int sellerId);

        [OperationContract]
        IEnumerable<Auction> GetAuctionsWhereWinnerId(int winnerId);

        [OperationContract]
        IEnumerable<Auction> GetAuctionsWithLessTimeLeft();

        [OperationContract]
        IEnumerable<Auction> GetMostRecentAuctions();
        [OperationContract]
        Auction GetAuctionByIdWithObjects(int Id);

        #endregion

        #region CategoryService
        [OperationContract]
        Category GetCategorytById(int Id);

        [OperationContract]
        int AddCategory(Category Category);

        [OperationContract]
        ICollection<Category> GetAllCategories();

        [OperationContract]
        void RemoveCategory(Category Category);

        [OperationContract]
        int RemoveCategoryById(int Id);

        [OperationContract]
        int UpdateCategory(Category Category);

        [OperationContract]
        Category GetCategoryByName(string name);
        [OperationContract]
        int RemoveCategoryByName(string name);
        [OperationContract]
        Category GetCategoryWithObjects(int Id);
        #endregion

        #region BidService
        [OperationContract]
        Bid GetBidById(int Id);
        [OperationContract]
        Bid CreateBid(double price, Account bidOwner, int auctionId);

        [OperationContract]
        int AddBid(Bid Bid);

        [OperationContract]
        ICollection<Bid> GetAllBids();
        [OperationContract]
        IEnumerable<Auction> GetAllAuctionsForBids(IEnumerable<Bid> bids);

        [OperationContract]
        void RemoveBid(Bid Bid);

        [OperationContract]
        int RemoveBidById(int Id);

        [OperationContract]
        int UpdateBid(Bid Bid);
        [OperationContract]
        IEnumerable<Bid> GetAllBidsByAccountId(int accountId);
        [OperationContract]
        IEnumerable<Bid> GetAllBidsByAuctionId(int auctionId);
        [OperationContract]
        Bid GetBidForAuctionWherePrice(double price, Auction auction);
        [OperationContract]
        double GetHighestPriceForAuction(Auction auction);
        [OperationContract]
        Bid GetBidWithObjects(int Id);
        [OperationContract]
        Bid GetBidWithObjectsWithAuctionId(int auctionId);
        [OperationContract]
        Bid GetBidWithObjectsWithAccountId(int accountId);
        #endregion

        #region ProductService

        [OperationContract]
        Product GetProductById(int Id);

        [OperationContract]
        IEnumerable<Product> GetAllProductsInCategory(int CategoryId);

        [OperationContract]
        int AddProduct(Product Product);

        [OperationContract]
        ICollection<Product> GetAllProducts();

        [OperationContract]
        void RemoveProduct(Product Product);

        [OperationContract]
        int RemoveProductById(int Id);

        [OperationContract]
        int UpdateProduct(Product Product);

        [OperationContract]
        IEnumerable<Product> FindProductsWithPriceMoreThan(double price);

        [OperationContract]
        IEnumerable<Product> GetProductsWithName(string name);

        [OperationContract]
        IEnumerable<Product> GetProductsWithDescription(string description);

        [OperationContract]
        IEnumerable<Product> FindProductsWithPriceLessThan(double price);
        [OperationContract]
        Product GetProductWithObjects(int Id);
        #endregion
    }

}
