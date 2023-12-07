using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DI_UT2_Practica2._2_Diseno_de_formulario
{
    internal class Checkers

    // Based in https://andresledo.es/csharp/validar-dni-nif-net/
    {

        internal bool checkNif(string nif)
        {
            try 
            {
                
                if (nif.Length != 9) return false; // EARLY RETURN


                int nifNumbers = int.Parse(nif.Substring(0, nif.Length - 1));
                string nifLetter = nif[nif.Length - 1] + "";

                if (checkLetterNif(nifNumbers) != nifLetter )
                {
                    throw new Exception();
                }

            }
            catch (Exception e)
            {
                return false; // EARLY RETURN
            }

            // If all is ok, send true
            return true;
        }

        private string checkLetterNif(int dniNumbers)
        {
            string[] letters = { "T", "R", "W", "A", "G", "M", "Y", "F", "P", "D", "X", "B", "N", "J", "Z", "S", "Q", "V", "H", "L", "C", "K", "E" };
            int mod = dniNumbers % 23; // 23 Letras
            return letters[mod];
        }
        
        internal bool containNumber(string name)
        {
            // If it's a number
            return Regex.IsMatch(name, "\\d+");
        }

        /*
         * -1 Invalid date
         * 0 is in blank
         * 1 is ok
         */
        internal int checkBirthDate(String date)
        {
            if (date == "") return 0;

            try
            {
                if (
                    DateTime.Parse(date) > DateTime.Now 
                    || 
                    DateTime.Parse(date) < DateTime.Parse("01/01/1900")
                    ||
                    date.ToString().Length != 10
                    )

                    throw new FormatException();
            }
            catch (FormatException)
            {
                return -1;
            }

            return 1;
        }

        internal bool checkPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 9) return false;

            return Regex.IsMatch(phoneNumber, "\\d{9}");
        }
        /*

         */
        internal bool checkEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return true;
            if ( Regex.IsMatch(email, "^[^@]+@[^@]+\\.[^@]{2,4}$")) return true;

            return false;
        }

    }
}
