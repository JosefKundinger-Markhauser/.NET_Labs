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

        static void Main(string[] args)
        {

            Program pro = new Program();
            String userInput = "";
            String message = "";
            DateTime startTime;
            DateTime endTime;
            IList<string> tempList;
            int num = 0;

            while (userInput.ToLower() != "x")
            {
                pro.PrintMenu(message);
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        num = pro.ImportWordsFromFile();
                        message = "Number of words = " + num;
                        break;
                    case "2":
                        startTime = DateTime.Now;
                        pro.BubbleSort(pro.words);
                        endTime = DateTime.Now;
                        message = "Time taken to bubble sort: " + (endTime - startTime);
                        break;
                    case "3":
                        startTime = DateTime.Now;
                        pro.LINQSort(pro.words);
                        endTime = DateTime.Now;
                        message = "Time taken to LINQ sort: " + (endTime - startTime);
                        break;
                    case "4":
                        num = pro.CountDistintWords(pro.words);
                        message = "Number of distinct words: " + num;
                        break;
                    case "5":
                        tempList = pro.Take10Words(pro.words);
                        message = "First 10 words = [";
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            message += tempList[i];
                            if (i == tempList.Count() - 1)
                            {
                                message += "]";
                            }
                            else
                            {
                                message += ", ";
                            }
                        }
                        break;
                    case "6":
                        num = pro.GetNumWordsWithJ(pro.words);
                        message = "Number of words that start with J: " + num;
                        break;
                    case "7":
                        tempList = pro.GetWordsEndWithD(pro.words);
                        message = "Number of words that end with D: " + tempList.Count() + "\n";
                        message += "Words that end with D = [";
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            message += tempList[i];
                            if (i == tempList.Count() - 1)
                            {
                                message += "]";
                            }
                            else
                            {
                                message += ", ";
                            }
                        }
                        break;
                    case "8":
                        tempList = pro.GetWordsGreaterThan4Chars(pro.words);
                        message = "Number of words greater than 4 chars: " + tempList.Count() + "\n";
                        message += "Words greater than 4 chars = [";
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            message += tempList[i];
                            if (i == tempList.Count() - 1)
                            {
                                message += "]";
                            }
                            else
                            {
                                message += ", ";
                            }
                        }
                        break;
                    case "9":
                        tempList = pro.GetWordsLessThan3CharsAndStartWithA(pro.words);
                        message = "Number of words less than 3 chars and starts with A: " + tempList.Count() + "\n";
                        message += "Words less than 3 chars and starts with A = [";
                        for (int i = 0; i < tempList.Count(); i++)
                        {
                            message += tempList[i];
                            if (i == tempList.Count() - 1)
                            {
                                message += "]";
                            }
                            else
                            {
                                message += ", ";
                            }
                        }
                        break;
                    default:
                        message = "Invalid Input";
                        break;
                }
            }            
        }

        void PrintMenu(string message)
        {
            Console.Clear();
            if (message == "Invalid Input")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(message);
            }
            
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

        int ImportWordsFromFile()
        {
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
            return numWords;
        }

        IList<string> BubbleSort(IList<string> words)
        {
            IList<string> newList = new List<string>();
            String temp;
            int i, j, l;

            newList = words;

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

        IList<string> LINQSort(IList<string> words)
        {
            IList<string> tempList = new List<string>();

            tempList = words.OrderBy(x => x).ToList();

            return tempList;
        }

        int CountDistintWords(IList<string> words)
        {
            IList<string> tempList = new List<string>();
            int numDistinct = 0;

            tempList = words.Distinct().ToList();

            numDistinct = tempList.Count();

            return numDistinct;
        }

        IList<string> Take10Words(IList<string> words)
        {
            IList<string> tempList = new List<string>();

            tempList = words.Take(10).ToList();

            return tempList;
        }

        int GetNumWordsWithJ(IList<string> words)
        {
            int num = 0;
            for (int i = 0; i < words.Count(); i++)
            {
                if (words[i].StartsWith("j"))
                {
                    num++;
                }
                else if (words[i].StartsWith("J"))
                {
                    num++;
                }
            }
            return num;
        }

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
