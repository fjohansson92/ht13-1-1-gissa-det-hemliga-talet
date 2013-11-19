using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NumberGuessingGame.ViewModels;

namespace NumberGuessingGame.Controllers
{
    public class NumberGuessingController : Controller
    {
        //
        // GET: /NumberGuessing/

        public ActionResult Index()
        {
            var model = new GuessingIndexViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(GuessingIndexViewModel guessingIndexViewModel)
        {
            return View(guessingIndexViewModel);
        }
        
    }
}
