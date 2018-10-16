using AuctionWebApplication.AuctionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AuctionWebApplication.Controllers
{
    public class AuctionController : Controller
    {

        IAuctionProjectService AccService = new AuctionProjectServiceClient("secure");

        #region GET METHODS
        // GET: Auction
        public ActionResult Index()
        {
            
            var collection = AccService.GetAllAuctionsWithObjects();
            return View(collection);
        }

        
         public ActionResult List(string Search,Product[] Products)
         {
            if (string.IsNullOrEmpty(Search))
            {
                return RedirectToAction("Index");
            }
            Product[] products;
            if (Products == null)
                products = AccService.GetProductsWithName(Search);
            else
                products = Products;

            var auctions = AccService.getAllAucionsForProducts(products);

            return View(auctions);
        }

        //Get
        public ActionResult Details(int Id)
        {
            var auc = AccService.GetAuctionByIdWithObjects(Id);
            if (auc != null)
            {
                string timeLeft;
                if (auc.EndTime > DateTime.Now)
                {
                    var left = (auc.EndTime - DateTime.Now);
                    timeLeft = auc.EndTime.ToString("dd-MM-yyyy h:mm:ss tt");
                }
                else
                    timeLeft = "Finished";          

                ViewBag.Rem_Time = timeLeft;

                return View(auc);
            }
            else
            {
                return View();
            }
        }

        public ActionResult DetailsWithProduct(int id)
        {
            var auc = AccService.GetAuctionWithProductId(id);
            return RedirectToAction("Details", new { Id = auc.Id });
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        //GET
        public ActionResult Edit(int Id)
        {
            var acc = AccService.GetAuctionById(Id);
            if (acc != null)
                return View(acc);
            else
                 return View();
        }

        public ActionResult Delete(int id)
        {
            var acc = AccService.GetAuctionById(id);
            if (acc != null)
                return View(acc);
            else
                return View();
        }

        #endregion

        #region POST METHODS

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StartTime,EndTime,")] AuctionService.Auction auc)
        {
            if (ModelState.IsValid && auc.StartTime > DateTime.Now)
            {
                var Seller = (Account)Session["LoggedUser"];
                auc.Seller = Seller;
                auc.CurrentHighestBid = 0;
                Session["Auction"] = auc;
                return RedirectToAction("Create", "Product");
            }
            else
            {
                TempData["ErrorMessage"] = " Wrong start date. Start date should be after current ";             
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            AccService.RemoveAuctionById(id);
            return RedirectToAction("Index", "Auction");
        }
        #endregion
    }
}