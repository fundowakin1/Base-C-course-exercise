using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Base_CSharp_course_exercise
{
    internal class TextParser
    {
        private static char[] separators = Enumerable.Range(char.MinValue, char.MaxValue - char.MinValue + 1)
            .Select(x => (char)x).Where(x => char.IsSeparator((x)) || char.IsPunctuation(x)).ToArray();
        public static List<string> SplittingText(string text)
        {
            return text.Trim().Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        /// <summary>
        /// Reading from text, finding position of the word in text
        /// </summary>
        /// <param name="path">path to the file</param>
        /// <param name="splittedText">is a List of string, which we will use in the CountingAndSortingWords method</param>
        /// <param name="words">is a List of Word type, which we get from the text with positions</param>
        
        public static void PuttingWordsToList(string path, List<string> splittedText, List<Word> words)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                var counter = 1;
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

        /// <summary>
        /// Counts words and put them into Dictionary and sorts it 
        /// </summary>
        /// <param name="splittedText">List which we are using to put words into Dictionary</param>
        /// <param name="numberOfWords">empty Dictionary in which we put word as key and number of occurrences of this word as value</param>
        public static void CountingAndSortingWords(List<string> splittedText, Dictionary<string, int> numberOfWords)
        {
            for (var i = 0; i < splittedText.Count; i++)
            {
                splittedText[i] = splittedText[i].ToLower();

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
            _ = numberOfWords.OrderByDescending(x => x.Value);
        }

        /// <summary>
        /// Outputting the whole information about word user typed
        /// </summary>
        /// <param name="numberOfWords">Dictionary from which we get the word's number of occurrences in the text</param>
        /// <param name="words">List from which we get the information about word's position in text</param>
        /// <param name="foundWord">word we are looking for</param>
        public static void FindWord(Dictionary<string, int> numberOfWords, List<Word> words, string foundWord)
        {
            
            var wordOccurrences = numberOfWords.FirstOrDefault(x => x.Key.ToLower() == foundWord.ToLower()).Value;
            if (wordOccurrences == 0)
            {
                Console.WriteLine("Word is not found!\n");
                return;
            }
            else
            {
                Console.WriteLine($"Word {foundWord} was met in text {wordOccurrences} times\n");
            }

            foreach (var word in words)
            {
                if (foundWord.ToLower() == word.Name.ToLower())
                {
                    Console.WriteLine($"Word \"{foundWord}\" occured in {word.Line} line at {word.Position} position");
                }
            }
            Console.WriteLine('\n');

        }
    }
}