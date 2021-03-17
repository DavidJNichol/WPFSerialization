using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFOOP2
{
    [Serializable]
    public class Model
    {
        public int timesClicked { get; set; }
        public string headsTailsString { get; set; }
        public int timesFlipped { get; set; }
        public string nameString { get; set; }
        public string dateString { get; set; }
        public int firstNumber { get; set; }
        public int secondNumber { get; set; }
        private int result { get; set; }
        public string resultString { get; set; }

        private Random rand;
        
        public void IncrementTimesClicked()
        {
            timesClicked++;
        }

        public void SetHeadsOrTails()
        {
            timesFlipped++;

            rand = new Random();

            if(rand.Next(0,2) == 0)
            {
                headsTailsString = "Heads";
            }
            else
            {
                headsTailsString = "Tails";
            }
        }

        public void SetDate()
        {
            dateString = DateTime.Now.ToString();
        }

        public void SetRandomNumbers()
        {
            rand = new Random();
            firstNumber = rand.Next(0, 51);
            rand = new Random();
            secondNumber = rand.Next(0, 51);

            result = firstNumber + secondNumber;
        }

        public void SetResultString(string answer)
        {
            //check answers
            if(answer.Equals(result.ToString()))
            {
                resultString = "Correct!";
            }
            else
            {
                resultString = "Incorrect!";
            }
        }

    }
}
