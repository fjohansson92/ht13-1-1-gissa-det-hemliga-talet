using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NumberGuessingGame.ViewModels
{
    public class GuessingIndexViewModel
    {
        [Required(ErrorMessage = "Du måste göra en gissning.")]
        [Range(1, 100, ErrorMessage = "Talet måste vara mellan 1 och 100.")]
        public int Guess { get; set; }
    }
}