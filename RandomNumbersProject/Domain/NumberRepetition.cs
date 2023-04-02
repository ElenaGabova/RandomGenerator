using Domain.Properties;
using System.ComponentModel.DataAnnotations;
using static Constants.Constants;

namespace Entities
{
    public class NumberRepetition
    {

        private int numberValue;
        private int repetitionAmount;

        public int NumberValue
        {
            get
            {
                return numberValue;
            }
            set
            {
                setNumber(value);
            }
        }

        public int RepetitionAmount {
            get
            {
                return repetitionAmount;
            }
            set
            {
                setRepetitionNumber(value);
            }
        }

        public NumberRepetition(int number, int repetitionAmount)
        {
            setNumber(number);
            setRepetitionNumber(repetitionAmount);
        }

        private void setRepetitionNumber(int value)
        {
            if (value < MinNumber || value > MaxNumber)
                throw new ArgumentException(string.Format(Resources.NotValidNumber, MinNumber, MaxNumber));

            this.repetitionAmount = value;
        }

        public void setNumber(int value)
        {
            if (value < MinNumber || value > MaxNumber)
                throw new ArgumentException(string.Format(Resources.NotValidNumber, MinNumber, MaxNumber));

            this.numberValue = value;
        }
    }
}