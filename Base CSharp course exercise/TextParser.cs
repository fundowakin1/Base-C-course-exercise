using System;
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

        public static void PuttingTextToDictionary(string path, List<string> splittedText, List<Word> words)
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
                        words.Add(temporaryWord);
                    }
                    splittedText.AddRange(temp);

                    counter++;
                }
            }
        }
    }
}
