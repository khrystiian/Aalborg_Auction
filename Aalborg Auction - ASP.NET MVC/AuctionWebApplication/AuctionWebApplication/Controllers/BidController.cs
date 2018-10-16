using AuctionWebApplication.AuctionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace AuctionWebApplication.Controllers
{
    public class BidController : Controller
    {
        IAuctionProjectService AccService = new AuctionProjectServiceClient("secure");

        #region GET METHODS
        // GET: Bid
        public ActionResult Index(int id)
        {
             return View(AccService.GetAllBidsByAuctionId(id));
        }

        public ActionResult Create(int id)
        {
            Auction auc = null;
            Bid bid = null;
                bid = AccService.GetBidWithObjectsWithAuctionId(id);
                auc = AccService.GetAuctionByIdWithObjects(id);
                if (bid == null)
                {
                    bid = new Bid();
                    bid.Auction = auc;
                    bid.AuctionId = auc.Id;
                }
            
            ViewBag.Rem_Time = auc.EndTime.ToString("dd-MM-yyyy h:mm:ss tt"); ;
            return View(bid);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(double price)
        {
            int id = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
            var acc = (Account) Session["LoggedUser"];
            if (acc != null)
            {
                var bid = AccService.CreateBid(price, acc, id);
                if (AccService.AddBid(bid) > 0)
                    return RedirectToAction("Index", new { id = id });

                TempData["ErrorMessage"] = "In order to bid you have to insert more than the current highest bid";
            }else
            {
                TempData["ErrorMessage"] = "You have to be logged in order to bid";                       
            }
            return RedirectToAction("Create", new { id = id });
        }

        public ActionResult Edit(int Id)
        {
            var bid = AccService.GetBidById(Id);
            return View(bid);
        }

        public ActionResult Details(int Id)
        {
            var bid = AccService.GetBidById(Id);
            return View(bid);
        }
        #endregion

    }
}