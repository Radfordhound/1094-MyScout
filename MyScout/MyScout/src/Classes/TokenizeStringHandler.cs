﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyScout
{
    static class TokenizeStringHandler
    {
        /// <summary>
        /// Converts a list of objects into a single comma-separated string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CreateTokenizedString(List<object> input)
        {
            string output = "";

            for (int i = 0; i < input.Count; i++)
            {
                output += (output.Length > 0 ? ":" : "") + input[i].ToString();
            }

            return output;
        }

        /// <summary>
        /// Converts a tokenized string back into a List of objects
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<object> ReadTokenizedString(string input)
        {
            List<object> output = new List<object>();
            int parsedNum;
            bool parsedBool;

            foreach (string s in input.Split(':'))
            {
                if (int.TryParse(s, out parsedNum))
                    output.Add(parsedNum);
                else if (bool.TryParse(s, out parsedBool))
                    output.Add(parsedBool);
                else
                    output.Add(s.ToString());
            }

            return output;
        }
    }
}
