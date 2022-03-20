﻿using System;
using System.Collections.Generic;
using System.Linq;

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

                Dictionary<string, int> numberOfWords = new();

                TextParser.CountingAndSortingWords(splittedText, numberOfWords);

                Console.WriteLine("\n\n----------------------------------------------------------");
                Console.WriteLine("Statistics:");
                Console.WriteLine("----------------------------------------------------------\n");
                Console.WriteLine("|------------------|");
                foreach (KeyValuePair<string, int> word in numberOfWords.OrderByDescending(x => x.Value))
                {
                    Console.WriteLine("|{0,-10} | {1,-5}|", word.Key, word.Value);
                    Console.WriteLine("|------------------|");
                }
                var findedWord = " ";

                while (findedWord != "-1")
                {
                    Console.WriteLine("Type your word to see statistics: (type \"-1\" to turn off programm)");
                    findedWord = Console.ReadLine();
                    if (findedWord == "-1")
                    {
                        Console.WriteLine("Have a nice day");
                        Environment.Exit(0);
                    }
                    TextParser.FindWord(numberOfWords, words, findedWord);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}