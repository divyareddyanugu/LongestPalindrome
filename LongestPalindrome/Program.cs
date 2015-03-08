/*************************************************************************************************/
/* Program              : Find the longest palindrome in a given string
 * Author               : Divya Reddy Anugu
 * Date                 : 01/25/2011
 * Version              : 1.0
 * Last Revision Date   : 01/25/2011
 * References           : Dynamic Programming Algorithm for finding the Longest Common Substring
/*************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LongestPalindrome
{
    class Program
    {
        public static void Main(string[] args)
        {
            string inputString;
            string inputStringRev;
            List<string> longestPalindromeList;

            //Read the input from the text file
            try
            {
                // Create an instance of StreamReader to read from a file.
                using (StreamReader sr = new StreamReader("../../Input.txt"))
                {
                    string line;
                    inputString = string.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        inputString = line;
                    }
                }

                //Reverse the input string and store it in inputStringRev
                Array inputCharArr = inputString.ToCharArray();
                Array.Reverse(inputCharArr);
                StringBuilder strbuild = new StringBuilder();
                foreach (char c in inputCharArr)
                {
                    strbuild.Append(c);
                }
                inputStringRev = strbuild.ToString();

                //Pass the input string and its reverse to the function
                longestPalindromeList = GetLongestCommonString(inputString, inputStringRev);

                Console.WriteLine("The longest palindrome(s) in the input string: \n ");
                foreach (string palindrome in longestPalindromeList)
                {
                    Console.WriteLine(palindrome + "\n");
                }
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }

            catch (FileNotFoundException fex)
            {
                Console.WriteLine("The file with the input string could not be found");
                Console.WriteLine(fex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// The function finds out the longest common substring between the two given strings.
        /// When a string and it's reverse string are passed as parameters, the function returns the  
        /// longest palindrome in the given string.
        /// </summary>
        /// <param name="firstString">The input string to the palindrome problem</param>
        /// <param name="secondString">The reverse of the input string</param>
        /// <returns>The longest common substring of the two input parameters</returns>
        public static List<string> GetLongestCommonString(string firstString, string secondString)
        {
            //Table to store the results of comparing the characters in firstString with secondString 
            int[,] dpTable = new int[firstString.Length, secondString.Length];

            char[] firstCharArray = firstString.ToCharArray();
            char[] secondCharArray = secondString.ToCharArray();
            StringBuilder lcs = new StringBuilder();

            //Indicates the length of the longest palindrome
            int maxLength = 0;

            //Stores the palindromes whose length is equal to 'maxlength'
            List<string> palindromeList = new List<string>();

            //Store the indices of the palindromes
            List<int> lcsIndex = new List<int>();

            for (int i = 0; i < firstString.Length; i++)
            {
                for (int j = 0; j < secondString.Length; j++)
                {
                    //Check if the character in the first string matches the corresponding character
                    //in the second string
                    if (firstCharArray[i] == secondCharArray[j])
                    {
                        if ((i == 0) || (j == 0))
                        {
                            dpTable[i, j] = 1;
                        }
                        else
                        {
                            dpTable[i, j] = dpTable[i - 1, j - 1] + 1;

                            //If the entered value in the table is greater than the previous longest palindrome, update the 
                            //maxlength parameter and clear the list of indexes 
                            if (dpTable[i, j] > maxLength)
                            {
                                maxLength = dpTable[i, j];
                                lcsIndex.Clear();
                                lcsIndex.Add(i);
                            }

                            //If the entered value in the table is same as the previous longest palindrome, 
                            //add the index to the list
                            if (dpTable[i, j] == maxLength)
                            {
                                if (!lcsIndex.Contains(i))
                                {
                                    lcsIndex.Add(i);
                                }
                            }

                        }
                    }
                }

            }

            //Get the strings identified by the indices stored in lcsIndex and of length maxlength
            foreach (int index in lcsIndex)
            {
                for (int x = index - maxLength + 1; x <= index; x++)
                {
                    lcs = lcs.Append(firstCharArray[x]);
                }
                palindromeList.Add(lcs.ToString());
                lcs.Clear();
            }
            return palindromeList;
        }
    }

}
