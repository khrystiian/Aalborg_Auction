using AuctionWebApplication.AuctionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuctionWebApplication.Controllers
{
    public class CategoryController : Controller
    {
        IAuctionProjectService AccService = new AuctionProjectServiceClient("secure");

        // GET: Category
        public ActionResult Index()
        {
            return View(AccService.GetAllCategories());
        }
    }
}