using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1
{
    class Program
    {

        IList<string> words = new List<string>();
        int totalNumWords;

        static void Main(string[] args)
        {

            Program pro = new Program();
            String userInput = "";
            DateTime startTime;
            DateTime endTime;
            IList<string> tempList;
            int num;

            while (userInput.ToLower() != "x")
            {
                pro.PrintMenu();
                userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case "1":
                        pro.ImportWordsFromFile();
                        break;
                    case "2":
                        startTime = DateTime.Now;
                        pro.BubbleSort(pro.words);
                        endTime = DateTime.Now;
                        Console.WriteLine("Time taken to bubble sort: " + (endTime - startTime).TotalMilliseconds + "ms");
                        break;
                    case "3":
                        startTime = DateTime.Now;
                        pro.LINQSort(pro.words);
                        endTime = DateTime.Now;
                        Console.WriteLine("Time taken to LINQ sort: " + (endTime - startTime).TotalMilliseconds + "ms");
                        break;
                    case "4":
                        num = pro.CountDistintWords(pro.words);
                        Console.WriteLine("Number of distinct words: " + num);
                        break;
                    case "5":
                        tempList = pro.Take10Words(pro.words);
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            Console.WriteLine(tempList[i]);
                        }
                        break;
                    case "6":
                        tempList = pro.GetNumWordsWithJ(pro.words);
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            Console.WriteLine(tempList[i]);
                        }
                        Console.WriteLine("Number of words that start with 'j': " + tempList.Count());
                        break;
                    case "7":
                        tempList = pro.GetWordsEndWithD(pro.words);
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            Console.WriteLine(tempList[i]);
                        }
                        Console.WriteLine("Number of words that end with 'd': " + tempList.Count());
                        break;
                    case "8":
                        tempList = pro.GetWordsGreaterThan4Chars(pro.words);
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            Console.WriteLine(tempList[i]);
                        }
                        Console.WriteLine("Number of words longer than 4 characters: " + tempList.Count());
                        break;
                    case "9":
                        tempList = pro.GetWordsLessThan3CharsAndStartWithA(pro.words);
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            Console.WriteLine(tempList[i]);
                        }
                        Console.WriteLine("Number of words longer less than 3 characters and start with 'a': " + tempList.Count());
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Input");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }            
        }

        void PrintMenu()
        {
            Console.WriteLine("");
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

        // Option 1
        void ImportWordsFromFile()
        {
            Console.WriteLine("Reading Words");
            string pathToFile = "Words.txt";
            FileStream fs = new FileStream(pathToFile, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            string line = String.Empty;
            int numWords = 0;

            while ((line = sr.ReadLine()) != null)
            {
                words.Add(line);
                numWords++;
            }

            totalNumWords += numWords;
            Console.WriteLine("Reading Words complete");
            Console.WriteLine("Imported " + numWords + " Words");
            Console.WriteLine("There are " + totalNumWords + " Words total");
        }

        // Option 2
        IList<string> BubbleSort(IList<string> words)
        {
            IList<string> newList = new List<string>(words);
            String temp;
            int i, j, l;

            l = newList.Count();

            for (i = 0; i < l; i++)
            {
                for(j = 0; j < l-1; j++)
                {
                    if (newList[j].CompareTo(newList[j + 1]) > 0)
                    {
                        temp = newList[j];
                        newList[j] = newList[j + 1];
                        newList[j + 1] = temp;
                    }
                }
            }
            return newList;
        }

        // Option 3
        IList<string> LINQSort(IList<string> words)
        {
            IList<string> tempList = new List<string>();

            tempList = words.OrderBy(x => x).ToList();

            return tempList;
        }

        // Option 4
        int CountDistintWords(IList<string> words)
        {
            IList<string> tempList = new List<string>();
            int numDistinct = 0;

            tempList = words.Distinct().ToList();

            numDistinct = tempList.Count();

            return numDistinct;
        }

        // Option 5
        IList<string> Take10Words(IList<string> words)
        {
            IList<string> tempList = new List<string>();

            tempList = words.Take(10).ToList();

            return tempList;
        }

        // Option 6
        IList<string> GetNumWordsWithJ(IList<string> words)
        {
            IList<string> tempList = new List<string>();

            for (int i = 0; i < words.Count(); i++)
            {
                if (words[i].StartsWith("j"))
                {
                    tempList.Add(words[i]);
                }
                else if (words[i].StartsWith("J"))
                {
                    tempList.Add(words[i]);
                }
            }
            return tempList;
        }

        // Option 7
        IList<string> GetWordsEndWithD(IList<string> words)
        {
            IList<string> tempList = new List<string>();

            for (int i = 0; i < words.Count(); i++)
            {
                if (words[i].EndsWith("d"))
                {
                    tempList.Add(words[i]);
                }
                else if (words[i].EndsWith("D"))
                {
                    tempList.Add(words[i]);
                }
            }

            return tempList;
        }

        // Option 8
        IList<string> GetWordsGreaterThan4Chars(IList<string> words)
        {
            IList<string> tempList = new List<string>();

            for (int i = 0; i < words.Count(); i++)
            {
                if (words[i].Count() > 4)
                {
                    tempList.Add(words[i]);
                }
            }

            return tempList;
        }

        // Option 9
        IList<string> GetWordsLessThan3CharsAndStartWithA(IList<string> words)
        {
            IList<string> tempList = new List<string>();

            for (int i = 0; i < words.Count(); i++)
            {
                if (words[i].Count() < 3 && words[i].StartsWith("a"))
                {
                    tempList.Add(words[i]);
                }
                else if (words[i].Count() < 3 && words[i].StartsWith("A"))
                {
                    tempList.Add(words[i]);
                }
            }

            return tempList;
        }
    }
}
