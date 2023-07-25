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
            return text.Length;
        }

        public int CountCharactersExcludingWhitespace()
        {
            return text.Count(c => !char.IsWhiteSpace(c));
        }

        public int CountWords()
        {
            return words.Length;
        }

        public string FindMostUsedWord()
        {
            return words.GroupBy(w => w).OrderByDescending(g => g.Count()).First().Key;
        }

        public char FindMostUsedCharacter()
        {
            return text.GroupBy(c => c).OrderByDescending(g => g.Count()).First().Key;
        }
    }
}
