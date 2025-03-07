using System;

namespace SpanishAppGame.ScrambleWordsGame
{
    class ScrambleWords
    {
        static string[] words = { "gato", "perro", "libro", "casa", "amigo" };

        public static void StartGame()
        {
            Random random = new Random();
            string word = words[random.Next(words.Length)];
            char[] scrambled = word.ToCharArray();
            random.Shuffle(scrambled);

            Console.WriteLine("Unscramble this word: " + new string(scrambled));
            string guess = Console.ReadLine() ?? "";


            if (guess == word)
                Console.WriteLine("¡Correcto! 😎");
            else
                Console.WriteLine("Incorrecto. La palabra correcta era: " + word);
        }
    }

    static class Extensions
    {
        public static void Shuffle(this Random rng, char[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                var temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
