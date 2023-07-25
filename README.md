# WordCountChallenge
    class TextAnalyzer
    {
        private string text;
        private string[] words;

        public TextAnalyzer(string text)
        {
            this.text = text;
            this.words = GetWords(text);
        }

        // Create a new array splitting the text into words by spaces and line breaks,
        // removing any parts that are empty strings.
        // The Split method is overloaded. This one takes an array of char values that are used as separators.
        // StringSplitOptions.RemoveEmptyEntries ensures that any strings of "" are not counted as words.
        
        private string[] GetWords(string text)
        {
            return text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        // Replace("\r\n", "\n") replaces each carriage return-newline sequence 
        // with a single newline, so each line break is counted as a single character. 
        
        public int CountTotalCharacters()
        {
            return text.Replace("\r\n", "\n").Length;
        }

        // whitespace characters include spaces, tabs, and line breaks. 
        // It's crazy how powerful C# becomes once you learn these static methods for datatypes like char
        
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
            var (mostUsedCharacter, charCount) = analyzer.FindMostUsedAlphaCharacter();

            Console.WriteLine($"Total Words: {analyzer.CountWords()}");
            Console.WriteLine($"Total Characters: {analyzer.CountTotalCharacters()}");
            Console.WriteLine($"Character count (minus line returns and spaces): {analyzer.CountCharactersExcludingWhitespace()}");
            Console.WriteLine($"Unique Words: {string.Join(", ", analyzer.GetUniqueWords())}");
            Console.WriteLine($"Unique Alpha Characters: {string.Join(", ", analyzer.GetUniqueAlphaCharacters())}");
            Console.WriteLine($"Most used word: {mostUsedWord} (used {count} times)");
            Console.WriteLine($"Most used character: {mostUsedChar} (used {charcount} times)");
            Console.WriteLine($"Most used alpha character: {mostUsedCharacter} (used {charCount} times)");


            Console.WriteLine();
        }
    }
}
