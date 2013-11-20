using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NumberGuessingGame.Models;

namespace NumberGuessingGame.ViewModels
{
    public class GuessingIndexViewModel
    {
        [Required(ErrorMessage = "Du måste göra en gissning.")]
        [Range(1, 100, ErrorMessage = "Talet måste vara mellan 1 och 100.")]
        public int Guess { get; set; }

        public SecretNumber secretNumber;

        public string Message
        {
            get
            {
                string message = "";
                switch (secretNumber.LastGuessedNumber.Outcome)
                {
                    case Outcome.OldGuess:
                        message = String.Format("Du har redan gissat på talet {0}!", secretNumber.LastGuessedNumber.Number);
                        break;
                    case NumberGuessingGame.Models.Outcome.NoMoreGuesses:
                        message = String.Format("{0} är för lågt. Inga fler gissningar! Det hemliga talet var {1}.", secretNumber.LastGuessedNumber.Number, secretNumber.Number);
                        break;
                    case NumberGuessingGame.Models.Outcome.High:
                        message = String.Format("{0} är för högt", secretNumber.LastGuessedNumber.Number);
                        break;
                    case NumberGuessingGame.Models.Outcome.Low:
                        message = String.Format("{0} är för lågt", secretNumber.LastGuessedNumber.Number);
                        break;
                    case NumberGuessingGame.Models.Outcome.Right:
                        string number = this.getNumber((int)secretNumber.Count);
                        message = String.Format("Grattis du klarade det på {0} försöket", number);
                        break;
                }
                return message;
            }
        }

        public string OutcomeText 
        {
            get
            {
                string outcome = "";
                if (secretNumber.CanMakeGuess)
                {
                    outcome = this.getNumber(secretNumber.Count + 1);

                    outcome += " gissningen";
                }
                else if (secretNumber.LastGuessedNumber.Outcome == Outcome.Right)
                {
                    outcome = "Rätt gissat!";
                }
                else 
                {
                    outcome = "Inga fler gissningar!";
                }

                return outcome;
            }
        }

        public string Title
        {
            get
            {
                string title;
                if (secretNumber.LastGuessedNumber.Outcome == Outcome.Right)
                {
                    title = "Rätt gissat!";
                }
                else
                {
                    title = "Gissa det hemliga talet";
                }

                return title;            
            }
        }

        private string getNumber(int num)
        {
            string number = "";
            switch (num)
            {
                case 1:
                    number = "Första";
                    break;
                case 2:
                    number = "Andra";
                    break;
                case 3:
                    number = "Tredje";
                    break;
                case 4:
                    number = "Fjärde";
                    break;
                case 5:
                    number = "Femte";
                    break;
                case 6:
                    number = "Sjätte";
                    break;
                case 7:
                    number = "Sjunde";
                    break;
            }
            return number;
        }
    }
}