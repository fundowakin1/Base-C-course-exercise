﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base_CSharp_course_exercise
{
    internal class TextParser
    {
        private static char[] separators = { ' ', '.', ',', '!', '?', '"', '\t', '\n', '\r' };
        public static List<string> SplittingText(string text)
        {
            return text.Trim().Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static void PuttingWordsToList(string path, List<string> splittedText, List<Word> words)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                int counter = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    var temp = TextParser.SplittingText(line);
                    foreach (var word in temp)
                    {
                        Word temporaryWord = new Word()
                        {
                            Line = counter,
                            Name = word,
                            Position = (line.IndexOf(word) + 1)

                        };
                        line = line.Remove(line.IndexOf(word), word.Length)
                            .Insert(line.IndexOf(word), new string(' ', word.Length));

                        words.Add(temporaryWord);
                    } 
                    splittedText.AddRange(temp);

                    counter++;
                }
            }
        }

        public static void CountingAndSortingWords(List<string> splittedText, Dictionary<string, int> numberOfWords)
        {
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
        }

        public static void FindWord(Dictionary<string, int> numberOfWords, List<Word> words, string findedWord)
        {
            Console.WriteLine("Word \"{0}\" was met in text {1} times\n",
                findedWord, 
                numberOfWords.FirstOrDefault(x => x.Key.ToLower() == findedWord.ToLower()).Value);
            foreach (var word in words)
            {
                if (findedWord.ToLower() == word.Name.ToLower())
                {
                    Console.WriteLine("Word \"{0}\" occured in {1} line at {2} position", 
                        findedWord, word.Line, word.Position);
                }
                
            }

        }
    }
}
