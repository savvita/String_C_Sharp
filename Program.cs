using System;
using System.Text;

namespace App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string originalStr = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give " +
                "you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human " +
                "happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue " +
                "pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain " +
                "pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. " +
                "To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any " +
                "right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no " +
                "resultant pleasure?";

            string sep = new string('=', 20);

            Console.WriteLine("Original string:");
            Console.WriteLine(originalStr);

            Console.WriteLine(sep);

            Console.WriteLine("Make shorter:");
            Console.WriteLine(MakeShorter(originalStr, "secret"));

            Console.WriteLine(sep);

            Console.WriteLine("Words:");
            string[] words = SplitAndSort(originalStr);
            PrintWords(words);

            Console.WriteLine(sep);
            Console.WriteLine("Replace letters z, s, c:");
            Console.WriteLine(ReplaceLetters(originalStr, new char[] { 'z', 'c', 's' }));

            Console.WriteLine(sep);
            Console.WriteLine("Paragraphs:");
            Console.WriteLine(MakeParagraphs(originalStr));
            
            Console.WriteLine(sep);
            Console.WriteLine("Replace \"I\" to \"same\":");
            Console.WriteLine(ReplaceWord(originalStr, "I", "same"));
        }

        static string MakeShorter(string text, string newWord)
        {
            string sep = " .,?!:;";

            int idx = 0; 
            int prev = 0;

            StringBuilder result = new StringBuilder();

            while (idx < text.Length)
            {
                prev = idx;

                while(prev < text.Length && sep.IndexOf(text[prev]) != -1)
                {
                    result.Append(text[prev++]);
                }

                idx = text.IndexOfAny(sep.ToCharArray(), prev);

                idx = (idx != -1) ? idx : text.Length;

                if(idx != prev && idx - prev < 3)
                {
                    result.Append(newWord);
                }
                else
                {
                    result.Append(text.Substring(prev, idx - prev));
                }
            }

            return result.ToString();
        }


        static string[] SplitAndSort(string text)
        {
            string sep = " .,?!:;";

            var words = text.Split(sep.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            QuickSort(words);

            return words;
        }

        static string ReplaceLetters(string text, char[] letters)
        {
            for (int i = 0; i < letters.Length; i++)
            {
                text = text.Replace(letters[i], Char.ToUpper(letters[i]));
            }

            return text;
        }
        
        static string ReplaceWord(string text, string oldWord, string newWord)
        {
            text = text.Replace(oldWord, newWord);

            return text;
        }

        static string MakeParagraphs(string text)
        {
            int idx = 0;
            while((idx = text.IndexOfAny(new char[] { '.', '?', '!'}, idx)) != -1)
            {
                idx++;

                if (idx != text.Length)
                {
                    text = text.Remove(idx, 1);
                    text = text.Insert(idx, "\n");
                }
            }
            return text;
        }

        static void QuickSort(string[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }
        static void QuickSort(string[] array, int left, int right)
        {
            int _left = left;
            int _right = right;
            string pivot = array[left];

            while (_left < _right)
            {
                while (array[_right].Length <= pivot.Length && _left < _right)
                    _right--;

                if (_left != _right)
                {
                    array[_left] = array[_right];
                    _left++;
                }

                while (array[_left].Length >= pivot.Length && _left < _right)
                    _left++;

                if (_left < _right)
                {
                    array[_right] = array[_left];
                    _right--;
                }

                array[_left] = pivot;
            }
            if (left < _left)
                QuickSort(array, left, _left - 1);

            if (right > _right)
                QuickSort(array, _right + 1, right);
        }

        static void PrintWords(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine(words[i]);
            }
        }
    }
}
