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

            for (int i = 0; i < splittedText.Count; i++)
            {
                splittedText[i] = splittedText[i].ToLower();
            }

            for (int i = 0; i < splittedText.Count; i++)
            {
                if (numberOfWords.ContainsKey(splittedText[i]))
                {
                    numberOfWords[splittedText[i]]++;
                    splittedText.Remove(splittedText[i]);
                }
                else
                {
                    numberOfWords.Add(splittedText[i], 1);
                }
            }

            foreach (KeyValuePair<string, int> word in numberOfWords.OrderByDescending(x => x.Value))
            {
                Console.WriteLine("{0} {1}", word.Key, word.Value);
            }

            Console.WriteLine("\n\n----------------------------------------------------------");
            Console.WriteLine("Statistics:");
            Console.WriteLine("----------------------------------------------------------\n\n");

            foreach (var word in words)
            {
                Console.WriteLine("|{0,-15}- line:{1,-5} position:{2,-5}|", word.Name, word.Line, word.Position);
                Console.WriteLine("|------------------------------------------|");
            }
            Console.WriteLine("----------------------------------------------------------");
        }
    }
}
