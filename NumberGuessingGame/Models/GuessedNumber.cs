using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberGuessingGame.Models
{
    // Struct is used for saving information about a guess.
    public struct GuessedNumber
    {
        public int? Number;
        public Outcome Outcome;

    }
}