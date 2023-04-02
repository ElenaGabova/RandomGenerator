using Domain.Properties;
using static Constants.Constants;

namespace Domain
{
    public class Number
    {
        private int numberValue;
        public int NumberValue
        {
            get
            {
                return numberValue;
            }
            set
            {
                SetNumber(value);
            }
        }

        public Number(int number)
        {
            SetNumber(number);
        }


        public void SetNumber(int value)
        {
            if (value < 0)
                throw new ArgumentException(Resources.NotValidRepetitionAmount);

            this.numberValue = value;
        }
    }
}