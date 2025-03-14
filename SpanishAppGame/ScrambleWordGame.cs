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
        private string[] words = { "gato", "perro", "libro", "casa", "amigo", "ciudad", "rio", "bosque", "playa",
    "escuela", "profesor", "estudiante", "computadora", "teclado", "raton", "pantalla"
};
        public ScrambleWordForm()
{
    Text = "🧩 Scramble Word Game";
    Width = 500; // Wider window
    Height = 400;
    StartPosition = FormStartPosition.CenterScreen;
    BackColor = Color.WhiteSmoke;
    FormBorderStyle = FormBorderStyle.FixedSingle;
    
    // Set a custom icon (Make sure you have an "icon.ico" file)
    Icon = new Icon("Resources/icon.ico");

    // Scrambled word label
    scrambledLabel = new Label
    {
        Text = "Scrambled word will appear here",
        Dock = DockStyle.Top,
        Font = new Font("Arial", 18, FontStyle.Bold),
        ForeColor = Color.DarkBlue,
        TextAlign = ContentAlignment.MiddleCenter,
        Height = 70,
        Padding = new Padding(10)
    };

    // Input box
    inputBox = new TextBox
    {
        Dock = DockStyle.Top,
        Font = new Font("Arial", 16),
        TextAlign = HorizontalAlignment.Center,
        BackColor = Color.LightYellow,
        ForeColor = Color.Black,
        BorderStyle = BorderStyle.FixedSingle,
        Height = 40,
        Margin = new Padding(10)
    };

    // Submit Button
    submitButton = new Button
    {
        Text = "✔ Submit",
        Dock = DockStyle.Top,
        Width = 140,
        Height = 50,
        BackColor = Color.SeaGreen,
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Font = new Font("Arial", 14, FontStyle.Bold)
    };
    submitButton.FlatAppearance.BorderSize = 0;
    submitButton.Click += OnSubmit;

    // New Word Button
    newWordButton = new Button
    {
        Text = "🔄 New Word",
        Dock = DockStyle.Top,
        Width = 140,
        Height = 50,
        BackColor = Color.CornflowerBlue,
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Font = new Font("Arial", 14, FontStyle.Bold)
    };
    newWordButton.FlatAppearance.BorderSize = 0;
    newWordButton.Click += OnNewWord;

    // Layout Panel for better spacing
    FlowLayoutPanel buttonPanel = new FlowLayoutPanel
    {
        Dock = DockStyle.Top,
        FlowDirection = FlowDirection.LeftToRight,
        Height = 60,
        Padding = new Padding(10),
        AutoSize = true
    };

    buttonPanel.Controls.Add(submitButton);
    buttonPanel.Controls.Add(newWordButton);

    Controls.Add(buttonPanel);
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
                MessageBox.Show("¡Correcto! 😎", "🎉 Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Incorrecto. La palabra correcta era: " + currentWord, "❌ Result", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OnNewWord(object sender, EventArgs e)
        {
            inputBox.Text = string.Empty;
            GenerateNewWord();
        }
    }
}
