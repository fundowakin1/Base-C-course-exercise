using System;
using System.Collections.Generic;
using System.Linq;

namespace Base_CSharp_course_exercise
{
    /*
     It is necessary to create an application that accepts the path to a text 
     file as input and performs splitting the text into words.
     All found words are formed into a dictionary that contains all 
     occurrences of the word in the text and the positions of these occurrences.
     After processing the text, the program should display statistics 
     on the words found and the frequency of their repetitions in the text, 
     the list should be sorted in descending order.
     After displaying general statistics, the program waits for the user 
     to enter a word and returns information about all occurrences of the word 
     in the text (line number and position).
     */
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please, input full path to your file:");
            string path = Console.ReadLine();
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
            string findedWord = " ";

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
    }
}