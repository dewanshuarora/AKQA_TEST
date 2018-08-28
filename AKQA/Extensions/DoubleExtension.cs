using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AKQA.Extensions
{
    public static class StringExtension
    {

        public static string ConvertToWords(this string n)
        {
            double intPart;
            double decPart = 0;
            if (double.TryParse(n, out intPart) == false)
            {
                return "INVALID INPUT";
            }
            string finalWords = "";
            try
            {
                //SPLITTING THE NUMBER INTO TWO PARTS, BEFORE DECIMAL AND AFTER DECIMAL
                string[] splitter = n.ToString().Split('.');
                if (string.IsNullOrEmpty(splitter[0]))
                {
                    intPart = 0;
                }
                else
                {
                    intPart = double.Parse(splitter[0]);
                }
                if (splitter.Length > 1 && string.IsNullOrEmpty(splitter[1]) == false)
                {
                    decPart = double.Parse(splitter[1]);
                }
            }
            catch
            {
                intPart = double.Parse(n);
            }

            if (intPart == 0 && decPart == 0)
                return "ZERO";

            //CONVERTING TO WORDS FOR THE INITIALI PART OF THE NUMBER
            if (intPart > 0)
            {
                finalWords = ToWords(intPart) + " DOLLARS";
            }

            //CONVERTING TO WORDS FOR THE DECIMAL PART OF THE NUMBER
            if (decPart > 0)
            {
                if (finalWords != "")
                    finalWords += " AND ";
                int counter = decPart.ToString().Length;
                finalWords += ToWords(decPart) + " CENTS";
            }
            return finalWords;
        }

        private static string ToWords(double number) //CONVERTS DOUBLE TO WORDS
        {
            string[] numbersArr = new string[] { "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
            string[] tensArr = new string[] { "TWENTY", "THIRTY", "FOURTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINTY" };
            string[] suffixesArr = new string[] { "THOUSAND", "MILLION", "BILLION", "TRILLION" };

            string finalWords = "";
            bool tens = false;

            if (number < 0)
            {
                finalWords += "NEGATIVE ";
                number *= -1;
            }

            // CONVERTION OF NUMBERS FROM TRILLION TO MILLION PLACES USING RECURSION
            int maxNumberPower = (suffixesArr.Length + 1) * 3;
            while (maxNumberPower > 3)
            {
                double power = Math.Pow(10, maxNumberPower);
                if (number >= power)
                {
                    if (number % power > 0)
                    {
                        finalWords += ToWords(Math.Floor(number / power)) + " " + suffixesArr[(maxNumberPower / 3) - 1] + " AND ";
                    }
                    else if (number % power == 0)
                    {
                        finalWords += ToWords(Math.Floor(number / power)) + " " + suffixesArr[(maxNumberPower / 3) - 1];
                    }
                    number %= power; //GETTING THE NUMBER TO THE NEXT LOWER POWER
                }
                maxNumberPower -= 3;
            }
            //CONVERTION IF LEFT OVER NUMBER IS STILL GREATER THAN THOUSAND
            if (number >= 1000)
            {
                if (number % 1000 > 0) finalWords += ToWords(Math.Floor(number / 1000)) + " THOUSAND AND ";
                else finalWords += ToWords(Math.Floor(number / 1000)) + " THOUSAND";
                number %= 1000; //GETTING THE NUMBER TO THOUSANDS PLACE
            }
            //CONVERTING TO HUNDREDS AND TENS PLACES
            if (0 <= number && number <= 999)
            {
                if ((int)number / 100 > 0)
                {
                    finalWords += ToWords(Math.Floor(number / 100)) + " HUNDRED";
                    number %= 100; //GETTING THE NUMBER TO THE HUNDREDS PLACE
                    if ((int)number / 10 > 1)
                    {
                        finalWords += " AND";
                    }
                }
                if ((int)number / 10 > 1)
                {
                    if (finalWords != "")
                        finalWords += " ";
                    finalWords += tensArr[(int)number / 10 - 2];
                    tens = true;
                    number %= 10;//GETTING THE NUMBER TO THE TENS PLACE
                }

                if (number < 20 && number > 0)
                {
                    if (finalWords != "" && tens == false)
                        finalWords += " ";
                    finalWords += (tens ? "-" + numbersArr[(int)number - 1] : numbersArr[(int)number - 1]);
                    number -= Math.Floor(number);
                }
            }

            return finalWords;

        }

    }
}