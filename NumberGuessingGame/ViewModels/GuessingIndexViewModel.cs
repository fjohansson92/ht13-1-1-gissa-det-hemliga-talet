using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using NumberGuessingGame.Models;
using NumberGuessingGame.Resources;

namespace NumberGuessingGame.ViewModels
{
    public class GuessingIndexViewModel
    {
        // The guess made by user.
        [Required(ErrorMessageResourceName = "GuessRequiredError", ErrorMessageResourceType = typeof(Resources.Strings))]
        [Range(1, 100, ErrorMessageResourceName = "GuessRangeError", ErrorMessageResourceType = typeof(Resources.Strings))]
        [Display(Name = "GuessName", ResourceType = typeof(Resources.Strings))]
        public int Guess { get; set; }

        public SecretNumber secretNumber;

        // Returns message describing outcome from guess.
        public string Message
        {
            get
            {
                string message = "";
                switch (secretNumber.LastGuessedNumber.Outcome)
                {
                    case Outcome.OldGuess:
                        message = String.Format(Strings.OldGuessMessage, secretNumber.LastGuessedNumber.Number);
                        break;
                    case NumberGuessingGame.Models.Outcome.High:
                        message = String.Format(Strings.HighGuessMessage, secretNumber.LastGuessedNumber.Number);
                        break;
                    case NumberGuessingGame.Models.Outcome.Low:
                        message = String.Format(Strings.LowGuessMessage, secretNumber.LastGuessedNumber.Number);
                        break;
                    case NumberGuessingGame.Models.Outcome.Right:
                        string number = this.getNumber((int)secretNumber.Count).ToLower();
                        return String.Format(Strings.RightGuessMessage, number);
                }
                if (!secretNumber.CanMakeGuess)
                {
                    message += String.Format(Strings.NoMoreGuessesMessage, secretNumber.Number);
                }

                return message;
            }
        }

        // Returns a text describing the current stage in the game.
        public string OutcomeText 
        {
            get
            {
                string outcome = "";
                if (secretNumber.CanMakeGuess)
                {
                    outcome = this.getNumber(secretNumber.Count + 1);

                    outcome += Strings.Guess;
                }
                else if (secretNumber.LastGuessedNumber.Outcome == Outcome.Right)
                {
                    outcome = Strings.RightGuess;
                }
                else 
                {
                    outcome = Strings.GameOver;
                }

                return outcome;
            }
        }

        // Returns string to be used as title.
        public string Title
        {
            get
            {
                string title;
                if (secretNumber.LastGuessedNumber.Outcome == Outcome.Right)
                {
                    title = Strings.RightGuess;
                }
                else
                {
                    title = Strings.GuessTitle;
                }

                return title;            
            }
        }

        // Converts a integer to a respective string. 
        private string getNumber(int num)
        {
            string number = "";
            switch (num)
            {
                case 1:
                    number = Strings.First;
                    break;
                case 2:
                    number = Strings.Second;
                    break;
                case 3:
                    number = Strings.Third;
                    break;
                case 4:
                    number = Strings.Forth;
                    break;
                case 5:
                    number = Strings.Fifth;
                    break;
                case 6:
                    number = Strings.Sixth;
                    break;
                case 7:
                    number = Strings.Seventh;
                    break;
            }
            return number;
        }

    }
}