using System;
using System.Collections.Generic;

namespace Base_CSharp_course_exercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please, input full path to your file:");
                var path = Console.ReadLine();
                if (path==null || !path.EndsWith(".txt"))
                {
                    throw new Exception("File is not appropriate! Try again");
                }
                Console.WriteLine("Your text:");

                List<string> splittedText = new();
                List<Word> words = new();

                TextParser.PuttingWordsToList(path, splittedText, words);

                var numberOfWords = TextParser.CountingAndSortingWords(splittedText);

                Console.WriteLine("\n\n----------------------------------------------------------");
                Console.WriteLine("Statistics:");
                Console.WriteLine("----------------------------------------------------------\n");
                Console.WriteLine("|------------------|");
                foreach (KeyValuePair<string, int> word in numberOfWords)
                {
                    Console.WriteLine($"|{word.Key,-10} | {word.Value,-5}|");
                    Console.WriteLine("|------------------|");
                }
                var foundWord = " ";

                while (foundWord != "-1")
                {
                    Console.WriteLine("Type your word to see statistics: (type \"-1\" to turn off programm)");
                    foundWord = Console.ReadLine();
                    if (foundWord == "-1")
                    {
                        Console.WriteLine("Have a nice day");
                        Environment.Exit(0);
                    }
                    TextParser.FindWord(numberOfWords, words, foundWord);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}