using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using Database;
using System.Linq;
using BussinessLayer;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private static Dbcontext context;//= new Dbcontext();
        private AccountRepository Arepository; //= new AccountRepository(context);
        private ProductRepository Prepository;//= new ProductRepository(context);
        private CategoryRepository Crepository; //= new CategoryRepository(context);
        BidRepository BidRepository;// = new BidRepository(context);
        AuctionRepository AuctionRepository;//= new AuctionRepository(context);
        AccountController accController;
        const int expectedResult = 1;
        public UnitTest1()
        {
        context = new Dbcontext();
        Arepository = new AccountRepository(context);
        Prepository = new ProductRepository(context);
        Crepository = new CategoryRepository(context);
        BidRepository = new BidRepository(context);
        AuctionRepository = new AuctionRepository(context);
        accController = new AccountController();
    }
        
        #region Objects
        public Category CreateAndAddTestCategory()
        {
            Category category = new Category { Name = "animals" };
            Crepository.Add(category);
            return category;
        }

        public Product CreateTestProduct()
        {
            Category category = CreateAndAddTestCategory();
            return new Product { Name = "motoretka", Description = "Working well", Picture = "dasda", Price = 200, CategoryId = category.Id, Category = category };
        }

        Account CreateTestAccount()
        {
            return new Account { Fname = "Petar", Lname = "Iliev", Password = "StrongLongHardPassword", Address = "Blegkilde", Email = "managerBg.com", PhoneNumber = "0512512", UserName = "userBoy", Balance = 20 };
        }

        Account CreateAndAddTestAccount()
        {
            var acc = CreateTestAccount();
            accController.Add(acc);
            return acc;
        }

        Auction CreateTestAuction()
        {
            return new Auction
            {
                CurrentHighestBid = 100,
                CurrentOwnerName = "Delyan",
                EndTime = DateTime.Now,
                StartTime = DateTime.Now.Add(TimeSpan.FromDays(1)),
            };
        }

        Auction CreateAndAddTestAuction()
        {
            var acc = CreateTestAuction();
            AuctionRepository.Add(acc);
            return acc;
        }
        
        public Product CreateAndAddTestProduct()
        {
            var pr = CreateTestProduct();
            Prepository.Add(pr);
            return pr;
        }

        Bid CreateTestBid()
        {
            var auc = CreateAndAddTestAuction();
            return new Bid { BidTime = DateTime.Now, Price = 200, };
        }

        Bid CreateAndAddTestBid()
        {
            var bid = CreateTestBid();
            BidRepository.Add(bid);
            return bid;
        }
        #endregion

        #region ProductTestDatabase

        [TestMethod]
        public void AddProduct()
        {
            var pr = CreateTestProduct();
            int actualResult = Prepository.Add(pr);
            Assert.IsTrue(expectedResult == actualResult);
            Prepository.RemoveById(pr.Id);
            Crepository.RemoveById(pr.CategoryId);
        }

        [TestMethod]
        public void TestGetProductById()
        {
            var pr = CreateAndAddTestProduct();
            int id = pr.Id;
            var product = Prepository.GetById(id);
            Assert.AreEqual(pr, product);
            Prepository.RemoveById(id);
            Crepository.RemoveById(pr.CategoryId);
        }
        [TestMethod]
        public void TestGetProductByName()
        {
            ProductControler ctrl = new ProductControler();
            var pr = CreateAndAddTestProduct();
            var product =  ctrl.GetProductsWithName(pr.Name).FirstOrDefault();
            Assert.AreEqual(pr, product);
            Crepository.RemoveById(pr.CategoryId);
        }
        [TestMethod]
        public void TestGetProductByDescription()
        {
            var pr = CreateAndAddTestProduct();
            var product = Prepository.GetProductsWithDescription(pr.Description).First();
            Assert.AreEqual(pr, product);
            Prepository.Remove(product);
            Crepository.RemoveById(pr.CategoryId);
        }
        [TestMethod]
        public void TestDeleteProduct()
        {
            var pr = CreateAndAddTestProduct();
            var catId = pr.CategoryId;
            int actualResult = Prepository.Remove(pr);
            Assert.IsTrue(expectedResult == actualResult);
            Crepository.RemoveById(catId);

        }
        [TestMethod]
        public void UpdateProduct()
        {
            string newName = "MOTORETKA";
            var pr = CreateAndAddTestProduct();
            var product = Prepository.GetById(pr.Id);
            product.Name = newName;
            Prepository.Update(product);
            var savedProduct = Prepository.GetById(product.Id);
            Assert.AreEqual(product, savedProduct);
            Crepository.RemoveById(savedProduct.CategoryId);
        }
        #endregion

        #region CategoryTestDatabase
        [TestMethod]
        public void AddCategory()
        {
            Category category = new Category { Name = "Bikes" };
            int actualResult = Crepository.Add(category);
            Assert.IsTrue(expectedResult == actualResult);
            Crepository.RemoveById(category.Id);
        }

        [TestMethod]
        public void GetCategoryById()
        {
            var category = CreateAndAddTestCategory();
            var testCategory = Crepository.GetById(category.Id);
            Assert.AreEqual(category, testCategory);
            Crepository.Remove(testCategory);
        }
        [TestMethod]
        public void GetCategoryByName()
        {
            var category = CreateAndAddTestCategory();
            var testCategory = Crepository.GetCategoryByName(category.Name);
            Assert.AreEqual(category, testCategory);
            Crepository.Remove(testCategory);
        }
        [TestMethod]
        public void UpdateCategory()
        {
            var category = CreateAndAddTestCategory();
            var testCategory = Crepository.GetById(category.Id);
            testCategory.Name = "bikes";
            Crepository.Update(testCategory);
            Assert.AreEqual(category, testCategory);
            Crepository.Remove(category);
        }


        #endregion

        #region AccountTestDatabase

        [TestMethod]
        public void TestAddAccount()
        {
            Account acc = CreateTestAccount();
            int actualResult = Arepository.Add(acc);
            Assert.IsTrue(expectedResult == actualResult);
            Arepository.RemoveById(acc.Id);
        }
        [TestMethod]
        public void GetAccountWithId()
        {
            var acc = CreateAndAddTestAccount();
            var testAccount = Arepository.GetById(acc.Id);
            Assert.AreEqual(acc, testAccount);
           Arepository.RemoveById(testAccount.Id);
        }

        public void UpdateAccount()
        {
            var acc = CreateAndAddTestAccount();
            var testAccount = Arepository.GetById(acc.Id);
            testAccount.Fname = "Peter";
            Arepository.Update(testAccount);
            var savedProduct = Arepository.GetById(testAccount.Id);
            Assert.AreEqual(testAccount, savedProduct);
            Arepository.RemoveById(testAccount.Id);
        }

        public void RemoveAccount()
        {
            var acc = CreateAndAddTestAccount();
            int actualResult = Arepository.Remove(acc);
            Assert.IsTrue(expectedResult == actualResult);
        }
        [TestMethod]
        public void GetAccountByUsername()
        {
            var acc = CreateAndAddTestAccount();
            var testAcc = Arepository.GetAccountByUsername(acc.UserName);
            Assert.AreEqual(acc, testAcc);
            Arepository.RemoveById(testAcc.Id);
        }
        [TestMethod]
        public void GetAccountByEmail()
        {
            var acc = CreateAndAddTestAccount();
            var testAcc = Arepository.GetAccountByEmail(acc.Email);
            Assert.AreEqual(acc, testAcc);
            Arepository.RemoveById(testAcc.Id);
        }
        #endregion

        #region AccountTestController
        [TestMethod]
        public void LoginSuccess()
        {
            var acc = CreateAndAddTestAccount();
            string passwordUsedForLogin = "StrongLongHardPassword";
            string username = acc.UserName;
            bool result =  accController.Login(acc, passwordUsedForLogin);
            Assert.IsTrue(result);
            accController.RemoveById(acc.Id);
        }
        [TestMethod]
        public void LoginFail()
        {
            var acc = CreateAndAddTestAccount();
            string passwordUsedForLogin = "LongPassword";
            string username = acc.UserName;
            bool result = accController.Login(acc, passwordUsedForLogin);
            Assert.IsFalse(result);
            accController.RemoveById(acc.Id);
        }

        #endregion

        #region AuctionTestDatabase
        [TestMethod]
        public void AddAuction()
        {
            var auction = CreateTestAuction();
            int ActualResult = AuctionRepository.Add(auction);
            Assert.IsTrue(expectedResult == ActualResult);
            AuctionRepository.Remove(auction);
        }
        [TestMethod]
        public void UpdateAuction()
        {
            var auction = CreateAndAddTestAuction();
            var testAuction = AuctionRepository.GetById(auction.Id);
            testAuction.CurrentOwnerName = "DELYAN";
            AuctionRepository.Update(testAuction);
            var savedAuction = AuctionRepository.GetById(testAuction.Id);
            Assert.AreEqual(savedAuction, testAuction);
            AuctionRepository.Remove(savedAuction);
        }
        [TestMethod]
        public void RemoveAuction()
        {
            var auction = CreateAndAddTestAuction();
            int actualResult = AuctionRepository.Remove(auction);
            Assert.IsTrue(actualResult == expectedResult);
        }
        [TestMethod]
        public void GetAuctionById()
        {
            var auction = CreateAndAddTestAuction();
            var testAuction = AuctionRepository.GetById(auction.Id);
            Assert.AreEqual(auction, testAuction);
            AuctionRepository.Remove(testAuction);
        }

        #endregion

        #region BidTestDatabase

        [TestMethod]
        public void AddBid()
        {
            var bid = CreateAndAddTestBid();
            int actualResult = BidRepository.Add(bid);
            Assert.IsTrue(actualResult == expectedResult);
            BidRepository.Remove(bid);
        }
        [TestMethod]
        public void GetBidById()
        {
            var bid = CreateAndAddTestBid();
            var savedBid = BidRepository.GetById(bid.Id);
            Assert.AreEqual(bid, savedBid);
            BidRepository.Remove(bid);
        }
        [TestMethod]
        public void UpdateBid()
        {
            var bid = CreateAndAddTestBid();
            var savedBid = BidRepository.GetById(bid.Id);
            savedBid.Price = 300;
            BidRepository.Update(savedBid);
            var testBid = BidRepository.GetById(savedBid.Id);
            Assert.AreEqual(testBid, savedBid);
            BidRepository.Remove(bid);
        }
        [TestMethod]
        public void RemoveBid()
        {
            var bid = CreateAndAddTestBid();
            int actualResult = BidRepository.Remove(bid);
            Assert.IsTrue(actualResult == expectedResult);
        }
        #endregion
    }




}

