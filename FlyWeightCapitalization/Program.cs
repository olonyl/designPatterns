using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FlyWeightCapitalization
{
    public class FormattedText
    {
        private readonly string plainText;
        private bool[] capitalize;

        public FormattedText(string plainText)
        {
            this.plainText = plainText; 
            capitalize = new bool[plainText.Length];    
        }

        public void Capitalize(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                capitalize[i] = true;
            }

        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var index = 0; index < plainText.Length; index++)
            {
                var c = plainText[index];
                sb.Append(capitalize[index] ? char.ToUpper(c) : c);
            }
            return sb.ToString();
        }
    }

    public class BetterFormattedText
    {
        private string plainText;
        private List<TextRange> formatting = new List<TextRange>(); 

        public BetterFormattedText(string plainText)
        {
            this.plainText = plainText;
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange { Start = start, End = end };
            formatting.Add(range);
            return range;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < plainText.Length; i++)
            {
                var c = plainText[i];
                foreach (var range in formatting)
                {
                    if (range.Covers(i) && range.Capitalize)
                        c = char.ToUpper(c);
                    sb.Append(c);
                }
            }
            return sb.ToString();   
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize, Bold, Italic;

            public bool Covers(int position)
            {
                return position >= Start && position <= End;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var ft = new FormattedText("This is a brave new world");
            ft.Capitalize(10, 15);
            Console.WriteLine(ft);

            var bft = new BetterFormattedText("This is a brave new world");
            bft.GetRange(10,15).Capitalize = true;
            Console.WriteLine(bft);

            var sentence = new Sentence("hello world");
            sentence[0].Capitalize = true;
            sentence[1].Capitalize = true;
            sentence[1].Capitalize = true;
            sentence[2].Capitalize = true;
            Console.WriteLine(sentence);
        }
    }

    public class Sentence
    {
        private List<WordToken> tokens = new List<WordToken>();
        private string plaingText;
        public Sentence(string plainText)
        {
            this.plaingText = plainText;    
        }

        public WordToken this[int index]
        {
            get
            {
                var token = new WordToken { Capitalize = true, Index = index };
                if(!tokens.Exists(f => f.Index == index))
                    tokens.Add(token);
                return token;
            }
        }

        public override string ToString()
        {
            var words = plaingText.Split(' ');

            foreach (var token in tokens)
            {
                if (token.Index < words.Length   && token.Capitalize)
                    words[token.Index] = words[token.Index].ToUpper();

            }

            return string.Join(' ', words);
        }

        public class WordToken
        {
            public int Index;
            public bool Capitalize;
        }
    }
}
