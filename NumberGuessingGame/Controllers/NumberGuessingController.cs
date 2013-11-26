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
        // Starts a new Numberguessinggame.

        public ActionResult Index()
        {
            SecretNumber secretNumber = null;
            if (Session["SecretNumber"] == null)
            {
                secretNumber = new SecretNumber();
                Session["SecretNumber"] = secretNumber;
            }
            else
            {
                secretNumber = Session["SecretNumber"] as SecretNumber;
            }

            secretNumber.Initialize();

            var model = new GuessingIndexViewModel();
            model.secretNumber = secretNumber;

            return View("Index", model);
        }

        //
        // POST: /NumberGuessing/
        // Takes care of a guess.

        [HttpPost]
        public ActionResult Index([Bind(Include="Guess")]GuessingIndexViewModel guessingIndexViewModel)
        {
            if (Session["SecretNumber"] == null || Session.IsNewSession)
            {
                return View("SessionError");
            }

            var secretNumber = Session["SecretNumber"] as SecretNumber;
            guessingIndexViewModel.secretNumber = secretNumber;

            if (ModelState.IsValid)
            {
                var outcome = secretNumber.MakeGuess(guessingIndexViewModel.Guess);
            }

            return View("Index", guessingIndexViewModel);
        }
        
    }
}
