using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {

        bool valid = true;

        static void Main(string[] args)
        {

            Program pro = new Program();
            String userInput = "";

            while (userInput.ToLower() != "x")
            {
                pro.PrintMenu(pro.valid);
                pro.valid = true;
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        pro.ImportWordsFromFile();
                    break;
                    default:
                        pro.valid = false;
                        break;
                }
            }            
        }

        void PrintMenu(bool valid)
        {
            Console.Clear();
            if (valid == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Input");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("Options");
            Console.WriteLine("------------------------------");
            Console.WriteLine("1 - Import Words from File");
            Console.WriteLine("2 - Bubble Sort words");
            Console.WriteLine("3 - LINQ/Lambda sort words");
            Console.WriteLine("4 - Count the Distinct Words");
            Console.WriteLine("5 - Take the first 10 words");
            Console.WriteLine("6 - Get the number of words that start with 'j' and display the count");
            Console.WriteLine("7 - Get and display of words that end with 'd' and display the count");
            Console.WriteLine("8 - Get and display of words that are greater than 4 characters long, and display the count");
            Console.WriteLine("9 - Get and display of words that are less than 3 characters long and start with the letter 'a', and display the count");
            Console.WriteLine("x – Exit");
            Console.WriteLine("");
            Console.Write("Make a selection: ");
        }

        void ImportWordsFromFile()
        {

        }
    }
}
