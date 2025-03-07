using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class HelloWorldForm : Form
{
    private Button startButton;
    private TextBox textBox;
    private Button submitButton;
    private Label resultLabel;
    private Label instructionLabel;
    private List<string> wordBank;
    private string currentWord;
    private Panel racetrackPanel;

    public HelloWorldForm()
    {
        Text = "Typing Game Launcher";
        Width = 400;
        Height = 300;

        startButton = new Button();
        startButton.Text = "Start Typing Game";
        startButton.Dock = DockStyle.Fill;
        startButton.Click += new EventHandler(OnStartButtonClick);

        Controls.Add(startButton);

        // Initialize the word bank
        wordBank = new List<string>
        {
            "hello", "world", "programming", "language", "computer", "keyboard", "mouse", "screen", "window", "application",
            "software", "hardware", "internet", "network", "database", "server", "client", "protocol", "algorithm", "function",
            "variable", "constant", "loop", "condition", "array", "list", "dictionary", "class", "object", "method"
        };
    }

    private void OnStartButtonClick(object sender, EventArgs e)
    {
        // Remove the start button
        Controls.Remove(startButton);

        // Add the racetrack panel
        racetrackPanel = new Panel();
        racetrackPanel.Height = 50;
        racetrackPanel.Dock = DockStyle.Top;
        racetrackPanel.BackColor = System.Drawing.Color.Gray;

        // Add the typing game controls
        instructionLabel = new Label();
        instructionLabel.Dock = DockStyle.Top;
        instructionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        textBox = new TextBox();
        textBox.Dock = DockStyle.Top;

        submitButton = new Button();
        submitButton.Text = "Submit";
        submitButton.Dock = DockStyle.Top;
        submitButton.Click += new EventHandler(OnSubmit);

        resultLabel = new Label();
        resultLabel.Dock = DockStyle.Top;
        resultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        Controls.Add(resultLabel);
        Controls.Add(submitButton);
        Controls.Add(textBox);
        Controls.Add(instructionLabel);
        Controls.Add(racetrackPanel);

        // Display the first word
        DisplayNewWord();
    }

    private void DisplayNewWord()
    {
        Random random = new Random();
        int index = random.Next(wordBank.Count);
        currentWord = wordBank[index];
        instructionLabel.Text = $"Type '{currentWord}' and press Submit:";
    }

    private void OnSubmit(object sender, EventArgs e)
    {
        if (textBox.Text.ToLower() == currentWord)
        {
            resultLabel.Text = "Correct!";
        }
        else
        {
            resultLabel.Text = "Try again.";
        }

        // Display a new word
        DisplayNewWord();
    }
}