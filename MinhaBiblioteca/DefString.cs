using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaBiblioteca
{
    public class DefString
    {
        public static string Left(string param, int length)
        {
            if (param == null) return null;
            try
            {
                //we start at 0 since we want to get the characters starting from the
                //left and with the specified lenght and assign it to a variable
                string result = param.Substring(0, length);
                //return the result of the operation
                return result;
            }
            catch { return param; }
        }
        public static string Right(string param, int length)
        {
            string result = "";
            try
            {
                //start at the index based on the lenght of the sting minus
                //the specified lenght and assign it a variable
                result = param.Substring(param.Length - length, length);
                //return the result of the operation
            }
            catch (Exception) { }
            return result;
        }

        public static string Mid(string param, int startIndex, int length)
        {
            string result = "";
            try
            {
                //start at the specified index in the string ang get N number of
                //characters depending on the lenght and assign it to a variable
                result = param.Substring(startIndex, length);
                //return the result of the operation
            }
            catch (Exception) { }
            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }

        public static string valoresIniciais(string Texto, int tamanho)
        {
            string input = Texto;

            string sub = input.Substring(0, tamanho);

            return sub;
        }

        //public static string valoresFinais(string Texto, int tamanho)
        //{
        //    string input = Texto;

        //    string sub = input.Substring(48, tamanho);

        //    return sub;
        //}
    }
}
