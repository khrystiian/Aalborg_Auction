using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Core;
using BussinessLayer;
using System.Collections.ObjectModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WcfServiceLibraryAuction
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [GlobalErrorBehavior(typeof(ErrorHandler))]
    class AuctionProjectService : IAuctionProjectService
    {
        public static void Main(string[] args)
        {
            using (ServiceHost serviceHost = new ServiceHost(typeof(AuctionProjectService)))
            {
                try
                {
                    // Open the ServiceHost to start listening for messages.
                    serviceHost.Open();

                    // The service can now be accessed.
                    Console.WriteLine("The service is ready.");
                    Console.WriteLine("Press <ENTER> to terminate service.");
                    Console.ReadLine();

                    // Close the ServiceHost.
                    serviceHost.Close();
                }
                catch (TimeoutException timeProblem)
                {
                    Console.WriteLine(timeProblem.Message);
                    Console.ReadLine();
                }
                catch (CommunicationException commProblem)
                {
                    Console.WriteLine(commProblem.Message);
                    Console.ReadLine();
                }catch(Exception)
                {
                    Console.Write("exception");
                }

            }
        }

        public void LogError(Exception e)
        {
            ErrorHandler handler = new ErrorHandler();
            handler.HandleError(e);
        }
        #region Account
        AccountController contr = new AccountController();
        public int AddAccount(Account Account)
        {
                return contr.Add(Account);
        }

        public Account GetAccountById(int Id)
        {

            return contr.getById(Id);
        }

        public ICollection<Account> GetAllAccounts()
        {

            return contr.getAll();
        }

        public void RemoveAccount(Account Account)
        {

            contr.Remove(Account);
        }

        public int RemoveAccountById(int Id)
        {

            return contr.RemoveById(Id);
        }

        public int UpdateAccount(Account Account)
        {

            return contr.Update(Account);
        }
        public Account GetAccountByUsername(string username)
        {

            return contr.GetAccountByUsername(username);
        }

        public Account GetAccountByEmail(string email)
        {

            return contr.GetAccountByEmail(email);
        }

        public Account GetAccountWithObjects(int Id)
        {
            return contr.GetByIdWithObjects(Id);
        }

        public Account HashAndSaltPassword(Account acc)
        {
            return contr.HashAndSaltPassword(acc);
        }

        public bool Login(Account acc, string password)
        {
            return contr.Login(acc, password);
        }
        #endregion

        #region Auction
        AuctionController AuctionContr = new AuctionController();
        public Auction GetAuctionById(int Id)
        {
            return AuctionContr.getById(Id);
        }

        public async Task<IEnumerable<Auction>> GetAllAuctionsAsyncable()
        {
           return await AuctionContr.GetAllAuctionsWithObjectsAsync();
        }
        public IEnumerable<Auction> GetAllAuctionsForBids(IEnumerable<Bid> bids)
        {
            return AuctionContr.GetAllAuctionsForBids(bids);
        }

        public IEnumerable<Auction> GetAllActiveAuctions()
        {
            return AuctionContr.GetAllActiveAuctions();
        }

        public ICollection<Auction> GetAllAuctionsWithObjects()
        {
            return AuctionContr.GetAllWithObjects().ToList();
        }

        public bool CheckIfActive(int Id)
        {
            return AuctionContr.CheckIfActive(Id);
        }

        public int AddAuction(Auction Auction)
        {
            return AuctionContr.Add(Auction);
        }

        public ICollection<Auction> GetAllAuctions()
        {
            return AuctionContr.getAll();
        }

        public IEnumerable<Auction> GetAuctionsWhereSellerId(int sellerId)
        {
            return AuctionContr.GetAuctionsWhereSellerId(sellerId);
        }

        public IEnumerable<Auction> getAllAucionsForProducts(Product[] products)
        {
            return AuctionContr.getAllAucionsForProducts(products);
        }

        public IEnumerable<Auction> GetAuctionsWhereWinnerId(int winnerId)
        {
            return AuctionContr.GetAuctionsWhereWinnerId(winnerId);
        }

        public IEnumerable<Auction> GetAuctionsWithLessTimeLeft()
        {
            return AuctionContr.GetAuctionsWithLessTimeLeft();
        }
        public Auction GetAuctionWithProductId(int productId)
        {
            return AuctionContr.GetAuctionWithProductId(productId);
        }

        public IEnumerable<Auction> GetMostRecentAuctions()
        {
            return AuctionContr.GetMostRecentAuctions();
        }

        public void RemoveAuction(Auction Auction)
        {
            AuctionContr.Remove(Auction);
        }

        public int RemoveAuctionById(int Id)
        {
            return AuctionContr.RemoveById(Id);
        }

        public int UpdateAuction(Auction Auction)
        {
            return AuctionContr.Update(Auction);
        }

        public Auction GetAuctionByIdWithObjects(int Id)
        {
            return AuctionContr.GetByIdWithObjects(Id);
        }
        #endregion

        #region Bid
        BidController BidController = new BidController();

        public int AddBid(Bid Bid)
        {
            return BidController.Add(Bid);
        }
        public Bid CreateBid(double price, Account bidOwner, int auctionId)
        {
            return BidController.CreateBid(price, bidOwner, auctionId);
        }

        public ICollection<Bid> GetAllBids()
        {
            return BidController.getAll();
        }

        public IEnumerable<Bid> GetAllBidsByAccountId(int accountId)
        {
            return BidController.GetAllBidsByAccountId(accountId);
        }

        public IEnumerable<Bid> GetAllBidsByAuctionId(int auctionId)
        {
            return BidController.GetAllBidsByAuctionId(auctionId);
        }

        public Bid GetBidWithObjectsWithAuctionId(int auctionId)
        {
            return BidController.GetBidWithObjectsWithAuctionId(auctionId);
        }

        public Bid GetBidForAuctionWherePrice(double price, Auction auction)
        {
            return BidController.GetBidForAuctionWherePrice(price, auction);
        }

        public double GetHighestPriceForAuction(Auction auction)
        {
            return BidController.GetBidWithHighestPrice(auction);
        }

        public Bid GetBidWithObjectsWithAccountId(int accountId)
        {
            return BidController.GetBidWithObjectsWithAccountId(accountId);
        }

        public Bid GetBidById(int Id)
        {
            return BidController.getById(Id);
        }

        public Bid GetBidWithObjects(int Id)
        {
            return BidController.getByIdWithObjects(Id);
        }

        public void RemoveBid(Bid Bid)
        {
            BidController.Remove(Bid);
        }

        public int RemoveBidById(int Id)
        {
            return BidController.RemoveById(Id);
        }

        public int UpdateBid(Bid Bid)
        {
            return BidController.Update(Bid);
        }
        #endregion

        #region Category
        CategoryController CategoryCtrl = new CategoryController();
        public int AddCategory(Category Category)
        {
            return CategoryCtrl.Add(Category);
        }

        public ICollection<Category> GetAllCategories()
        {
            return CategoryCtrl.getAll();
        }

        public Category GetCategoryByName(string name)
        {
            return CategoryCtrl.GetCategoryByName(name);
        }

        public Category GetCategorytById(int Id)
        {
            return CategoryCtrl.getById(Id);
        }

        public Category GetCategoryWithObjects(int Id)
        {
            return CategoryCtrl.GetByIdWithObjects(Id);
        }

        public void RemoveCategory(Category Category)
        {
            CategoryCtrl.Remove(Category);
        }

        public int RemoveCategoryById(int Id)
        {
            return CategoryCtrl.RemoveById(Id);
        }

        public int RemoveCategoryByName(string name)
        {
            return CategoryCtrl.RemoveCategoryByName(name);
        }

        public int UpdateCategory(Category Category)
        {
            return CategoryCtrl.Update(Category);
        }
        #endregion

        #region Product
        ProductControler ProductContrl = new ProductControler();
        public int AddProduct(Product Product)
        {
            return ProductContrl.Add(Product);
        }

        public IEnumerable<Product> GetAllProductsInCategory(int CategoryId)
        {
            return ProductContrl.GetAllProductsInCategory(CategoryId);
        }

        public IEnumerable<Product> FindProductsWithPriceLessThan(double price)
        {
            return ProductContrl.FindProductsWithPriceLessThan(price);
        }

        public IEnumerable<Product> FindProductsWithPriceMoreThan(double price)
        {
            return ProductContrl.FindProductsWithPriceLessThan(price);
        }

        public ICollection<Product> GetAllProducts()
        {
            return ProductContrl.getAll();
        }

        public Product GetProductById(int Id)
        {
            return ProductContrl.getById(Id);
        }

        public IEnumerable<Product> GetProductsWithDescription(string description)
        {
            return ProductContrl.GetProductsWithDescription(description);
        }

        public IEnumerable<Product> GetProductsWithName(string name)
        {
            return ProductContrl.GetProductsWithName(name);
        }

        public Product GetProductWithObjects(int Id)
        {
            return ProductContrl.GetByIdWithObjects(Id);
        }

        public void RemoveProduct(Product Product)
        {
            ProductContrl.Remove(Product);
        }

        public int RemoveProductById(int Id)
        {
            return ProductContrl.RemoveById(Id);
        }

        public int UpdateProduct(Product Product)
        {
            return ProductContrl.Update(Product);
        }
        #endregion
    }
    
}
