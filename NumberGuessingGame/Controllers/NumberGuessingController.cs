using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NumberGuessingGame.Models;
using NumberGuessingGame.ViewModels;

namespace NumberGuessingGame.Controllers
{
    public class NumberGuessingController : Controller
    {
        //
        // GET: /NumberGuessing/

        public ActionResult Index()
        {
            Session["SecretNumber"] = new SecretNumber();

            var model = new GuessingIndexViewModel();
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Index([Bind(Include="Guess")]GuessingIndexViewModel guessingIndexViewModel)
        {
            var secretNumber = Session["SecretNumber"] as SecretNumber;
            guessingIndexViewModel.secretNumber = secretNumber;

            if (ModelState.IsValid)
            {
                var outcome = secretNumber.MakeGuess(guessingIndexViewModel.Guess);

            }

            return View("Guess", guessingIndexViewModel);
        }
        
    }
}
