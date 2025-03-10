using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SpanishAppGame
{
    public class ScrambleWordForm : Form
    {
        private Label scrambledLabel;
        private TextBox inputBox;
        private Button submitButton;
        private Button newWordButton;
        private string currentWord = string.Empty;
        private string scrambledWord = string.Empty;
        private string[] words = { "gato", "perro", "libro", "casa", "amigo" };

        public ScrambleWordForm()
        {
            Text = "Scramble Word Game";
            Width = 400;
            Height = 300;
            StartPosition = FormStartPosition.CenterScreen;

            scrambledLabel = new Label
            {
                Text = "Scrambled word will appear here",
                Dock = DockStyle.Top,
                Font = new Font("Arial", 14),
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 50
            };

            inputBox = new TextBox
            {
                Dock = DockStyle.Top,
                Font = new Font("Arial", 14),
                TextAlign = HorizontalAlignment.Center
            };

            submitButton = new Button
            {
                Text = "Submit",
                Dock = DockStyle.Top,
                Height = 40
            };
            submitButton.Click += OnSubmit;

            newWordButton = new Button
            {
                Text = "New Word",
                Dock = DockStyle.Top,
                Height = 40
            };
            newWordButton.Click += OnNewWord;

            Controls.Add(newWordButton);
            Controls.Add(submitButton);
            Controls.Add(inputBox);
            Controls.Add(scrambledLabel);

            GenerateNewWord();
        }

        private void GenerateNewWord()
        {
            Random random = new Random();
            currentWord = words[random.Next(words.Length)];
            scrambledWord = new string(currentWord.ToCharArray().OrderBy(_ => random.Next()).ToArray());
            scrambledLabel.Text = "Unscramble this word: " + scrambledWord;
        }

        private void OnSubmit(object sender, EventArgs e)
        {
            if (inputBox.Text.ToLower() == currentWord)
            {
                MessageBox.Show("¡Correcto! 😎", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Incorrecto. La palabra correcta era: " + currentWord, "Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnNewWord(object sender, EventArgs e)
        {
            inputBox.Text = string.Empty;
            GenerateNewWord();
        }
    }
}
