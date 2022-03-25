using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Base_CSharp_course_exercise
{
    /* Проектный код должен содержать комментарии описывающие логику работы и входные/выходные параметры */
    internal class TextParser
    {
        /* Для определения разделителя можно использовать готовые методы Char.IsSeparator(), Char.IsPunctuation() */
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
                var counter = 1;
                /* В условии while вместо != null можно испольовать string.IsNullOrEmpty() и string.IsNullOrWhiteSpace() */
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

        /* В этой функции я вижу расчет кол-ва вхождений слова но не вижу логики сортировки.
         Для автоматической соритровки можно использовать SortedDictionary */
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
        }

        public static void FindWord(Dictionary<string, int> numberOfWords, List<Word> words, string findedWord)
        {
            if ((numberOfWords.FirstOrDefault(x => x.Key.ToLower() == findedWord.ToLower()).Value)==0)
            {
                Console.WriteLine("Word is not found!\n");
                return;
            }
            Console.WriteLine("Word \"{0}\" was met in text {1} times\n",
                findedWord,
                numberOfWords.FirstOrDefault(x => x.Key.ToLower() == findedWord.ToLower()).Value);
            /*
             * Блок кода можно было бы упростить и выполнить проход по массиву один раз.
             */
            var wordOccurrences = numberOfWords.FirstOrDefault(x => x.Key.ToLower() == findedWord.ToLower()).Value;
            if (wordOccurrences == 0)
            {
                Console.WriteLine("Word is not found!\n");
                return;
            }
            else
            {
                /* Для формирования строкового вывода можно использовать новый формат */
                Console.WriteLine($"Word {findedWord} was met in text {wordOccurrences} times\n");
            }

            foreach (var word in words)
            {
                if (findedWord.ToLower() == word.Name.ToLower())
                {
                    Console.WriteLine("Word \"{0}\" occured in {1} line at {2} position",
                        findedWord, word.Line, word.Position);
                }
            }
            Console.WriteLine('\n');

        }
    }
}