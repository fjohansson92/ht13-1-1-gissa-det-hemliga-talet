using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NumberGuessingGame.Models
{
    public class SecretNumber
    {

        private List<GuessedNumber> _guessedNumbers;

        private GuessedNumber _lastGuessedNumber;

        private int? _number;

        public const int MaxNumberOfGuesses = 7;

        public bool CanMakeGuess
        {
            get 
            {
                return this.Count < MaxNumberOfGuesses && this.LastGuessedNumber.Outcome != Outcome.Right;
            }
        }

        public int Count 
        {
            get
            {
                return this._guessedNumbers.Count();
            }
        }

        public IList<GuessedNumber> GuessedNumbers
        {
            get
            {
                return this._guessedNumbers.AsReadOnly();
            }
        }

        public GuessedNumber LastGuessedNumber
        {
            get 
            {
                return _lastGuessedNumber;
            }
        }

        public int? Number
        {
            get
            {
                return this.CanMakeGuess ? null : this._number;
            }
            private set
            {
                this._number = value;
            }
        }

        public void Initialize() 
        {
            this._guessedNumbers.Clear();
            Random random = new Random();
            this.Number = random.Next(1, 100);

            this._lastGuessedNumber = new GuessedNumber();
            this._lastGuessedNumber.Outcome = Outcome.Indefinite;
        }

        public Outcome MakeGuess(int guess)
        {
            if (guess > 100 || guess < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (!this.CanMakeGuess)
            {
                return Outcome.NoMoreGuesses;
            }
            else if (this._guessedNumbers.Exists(x => x.Number == guess))
            {
                return Outcome.OldGuess;
            }

            if (guess > _number)
            {
                this._lastGuessedNumber.Outcome = Outcome.High;
            }
            else if (guess < _number)
            {
                this._lastGuessedNumber.Outcome = Outcome.Low;
            }
            else
            {
                this._lastGuessedNumber.Outcome = Outcome.Right;
            }

            this._lastGuessedNumber.Number = guess;
            this._guessedNumbers.Add(this._lastGuessedNumber);
            return this._lastGuessedNumber.Outcome;
        }

        public SecretNumber() 
        {
            this._guessedNumbers = new List<GuessedNumber>(MaxNumberOfGuesses);
            this.Initialize();
        }
    }
}