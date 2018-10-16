using AuctionWebApplication.AuctionService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace AuctionWebApplication.Controllers
{
    public class ProductController : Controller
    {
        IAuctionProjectService AccService = new AuctionProjectServiceClient("secure");

        #region GET METHODS
        // GET: Product
        public ActionResult Index()
        {
            return View(AccService.GetAllProducts());
        }

        public ActionResult List(int CategoryId)
        {
            var products = AccService.GetAllProductsInCategory(CategoryId);

            var auctions = AccService.getAllAucionsForProducts(products);

            return RedirectToAction("Index", "Auction", new { Search = " ", Products = products });
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(AccService.GetAllCategories(), "Id", "Name");
            return View();
        }

        public ActionResult Details(int id)
        {
            var pr = AccService.GetProductById(id);
            return RedirectToAction("DetailsWithProduct", "Auction", new { id = pr.Id });
        }

        #endregion

        #region POST METHODS

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product pr)
        {
            if (Session["Auction"] != null)
            {      
                var auction = (Auction)Session["Auction"];
                auction.Product = pr;
                return RedirectToAction("Show");
            }
            throw new Exception();
        }

        public  ActionResult Show()
        {
            Product pr = null;
            if (Session["Auction"] != null)
            {
                var auction = (Auction)Session["Auction"];
                pr = auction.Product;
                if(pr.Picture != null)
                {
                    auction.Product.Picture = pr.Picture;
                    AccService.AddAuction(auction);
                    return RedirectToAction("Index", "Auction");
                }
            }
                return View(pr);
        }


        #endregion 

    }
}