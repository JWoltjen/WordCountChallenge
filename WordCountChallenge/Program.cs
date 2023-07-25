using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        private static string[] tests = new string[]
        {
            @"The test of the 
            best way to handle

multiple lines,   extra spaces and more.",
            @"Using the starter app, create code that will 
loop through the strings and identify the total 
character count, the number of characters
excluding whitespace (including line returns), and the
number of words in the string. Finally, list each word, ensuring it
is a valid word."
        };

        /* 
            First string (tests[0]) Values:
            Total Words: 14
            Total Characters: 89
            Character count (minus line returns and spaces): 60
            Most used word: the (2 times)
            Most used character: e (10 times)

            Second string (tests[1]) Values:
            Total Words: 45
            Total Characters: 276
            Character count (minus line returns and spaces): 230
            Most used word: the (6 times)
            Most used character: t (24 times)
        */

        static void Main(string[] args)
        {
            TextPrinter printer = new TextPrinter();
            foreach(string test in tests)
            {
                TextAnalyzer analyzer = new TextAnalyzer(test);
                printer.PrintResults(analyzer);
            }
            Console.ReadLine();
        }
    }

    class TextAnalyzer
    {
        private string text;
        private string[] words;

        public TextAnalyzer(string text)
        {
            this.text = text;
            this.words = GetWords(text);
        }

        private string[] GetWords(string text)
        {
            return text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public int CountTotalCharacters()
        {
            return text.Replace("\r\n", "\n").Length;
        }

        public int CountCharactersExcludingWhitespace()
        {
            return text.Count(c => !char.IsWhiteSpace(c));
        }

        public int CountWords()
        {
            return words.Length;
        }

        public (string word, int count) FindMostUsedWord()
        {
            var mostUsedWord = words.Where(w => !string.IsNullOrWhiteSpace(w))
                                    .Select(w => w.ToLower())
                                    .GroupBy(w => w).OrderByDescending(g =>                       
                                    g.Count()).First();

            return (mostUsedWord.Key, mostUsedWord.Count());
        }

        public (char c, int count) FindMostUsedCharacter()
        {
            var mostUsedChar = text.Where(c => !char.IsWhiteSpace(c)).
                                    GroupBy(c => c).OrderByDescending(g =>      
                                    g.Count()).First();

            return (mostUsedChar.Key, mostUsedChar.Count());
        }
    }

    class TextPrinter
    {
        public void PrintResults(TextAnalyzer analyzer)
        {
            var (mostUsedWord, count) = analyzer.FindMostUsedWord();
            var (mostUsedChar, charcount) = analyzer.FindMostUsedCharacter();

            Console.WriteLine($"Total Words: {analyzer.CountWords()}");
            Console.WriteLine($"Total Characters: {analyzer.CountTotalCharacters()}");
            Console.WriteLine($"Character count (minus line returns and spaces): {analyzer.CountCharactersExcludingWhitespace()}");
            Console.WriteLine($"Most used word: {mostUsedWord} (used {count} times)");
            Console.WriteLine($"Most used character: {mostUsedChar} (used {charcount} times)");
            Console.WriteLine();
        }
    }
}
