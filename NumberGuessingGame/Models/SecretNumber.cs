using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberGuessingGame.Models
{
    public class SecretNumber
    {
        // Contains previous allowed guesses
        private List<GuessedNumber> _guessedNumbers;

        // Represents last guess.
        private GuessedNumber _lastGuessedNumber;

        // The correct secret number.
        private int? _number;

        // Max number of guesses before game over.
        public const int MaxNumberOfGuesses = 7;

        // Returns if user is allowed to guess.
        public bool CanMakeGuess
        {
            get 
            {
                return Count < MaxNumberOfGuesses && LastGuessedNumber.Outcome != Outcome.Right;
            }
        }

        // Return number of guesses done.
        public int Count 
        {
            get
            {
                return _guessedNumbers.Count();
            }
        }

        // Return a read-only reference of the guesses made.
        public IList<GuessedNumber> GuessedNumbers
        {
            get
            {
                return _guessedNumbers.AsReadOnly();
            }
        }

        // Returns the last guess.
        public GuessedNumber LastGuessedNumber
        {
            get 
            {
                return _lastGuessedNumber;
            }
        }

        // Returns the secret number when the game is over.
        public int? Number
        {
            get
            {
                return CanMakeGuess ? null : this._number;
            }
            private set
            {
                _number = value;
            }
        }

        // Initialize the class.
        public SecretNumber()
        {
            _guessedNumbers = new List<GuessedNumber>(MaxNumberOfGuesses);
            Initialize();
        }

        // Initialize the class properties.
        public void Initialize() 
        {
            _guessedNumbers.Clear();
            Random random = new Random();
            Number = random.Next(1, 100);

            _lastGuessedNumber = new GuessedNumber();
            _lastGuessedNumber.Outcome = Outcome.Indefinite;
        }

        // Takes care of a guess and returns outcome.
        public Outcome MakeGuess(int guess)
        {
            if (guess > 100 || guess < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (!CanMakeGuess)
            {
                return Outcome.NoMoreGuesses;
            }

            _lastGuessedNumber.Number = guess;

            if (_guessedNumbers.Exists(x => x.Number == guess))
            {
                _lastGuessedNumber.Outcome = Outcome.OldGuess;
                return _lastGuessedNumber.Outcome;
            } else if (guess > _number)
            {
                _lastGuessedNumber.Outcome = Outcome.High;
            }
            else if (guess < _number)
            {
                _lastGuessedNumber.Outcome = Outcome.Low;
            }
            else
            {
                _lastGuessedNumber.Outcome = Outcome.Right;
            }

            _guessedNumbers.Add(_lastGuessedNumber);
            return _lastGuessedNumber.Outcome;
        }
    }
}