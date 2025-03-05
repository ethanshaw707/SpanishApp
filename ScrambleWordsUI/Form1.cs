using System;
using System.Windows.Forms;
using SpanishAppGame.ScrambleWordsGame;

namespace ScrambleWordsUI
{
    public partial class Form1 : Form
    {
        private string currentWord;
        private string scrambledWord;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateWord();
        }

        private void GenerateWord()
        {
            Random random = new Random();
            string[] words = { "gato", "perro", "libro", "casa", "amigo" };
            currentWord = words[random.Next(words.Length)];
            scrambledWord = new string(currentWord.ToCharArray());
            scrambledWord = Shuffle(scrambledWord);
            lblScrambled.Text = "Unscramble this word: " + scrambledWord;
        }

        private string Shuffle(string word)
        {
            Random rng = new Random();
            char[] array = word.ToCharArray();
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                var temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
            return new string(array);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string guess = txtGuess.Text;
            if (guess == currentWord)
                MessageBox.Show("¡Correcto! 😎");
            else
                MessageBox.Show("Incorrecto. La palabra correcta era: " + currentWord);
        }

        private void btnNewWord_Click(object sender, EventArgs e)
        {
            txtGuess.Text = "";
            GenerateWord();
        }
    }
}
