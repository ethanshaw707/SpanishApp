using System;
using System.Windows.Forms;
using SpanishAppGame;

public class SpanishGamesForm : Form
{
    private Button crosswordButton;
    private Button typingGameButton;
    private Button scrambleGameButton;
    private Button exitButton;
    private Label titleLabel;

    public SpanishGamesForm()
    {
        Text = "Spanish Games";
        Width = 400;
        Height = 300;

        titleLabel = new Label();
        titleLabel.Text = "Welcome to the Spanish Games";
        titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        titleLabel.Dock = DockStyle.Top;

        crosswordButton = new Button();
        crosswordButton.Text = "1. Spanish Crossword";
        crosswordButton.Dock = DockStyle.Top;
        crosswordButton.Click += OnCrosswordClick;

        typingGameButton = new Button();
        typingGameButton.Text = "2. Spanish Typing Game";
        typingGameButton.Dock = DockStyle.Top;
        typingGameButton.Click += OnTypingGameClick;

        scrambleGameButton = new Button();
        scrambleGameButton.Text = "3. Spanish Scramble Game";
        scrambleGameButton.Dock = DockStyle.Top;
        scrambleGameButton.Click += OnScrambleGameClick;

        exitButton = new Button();
        exitButton.Text = "4. Exit";
        exitButton.Dock = DockStyle.Top;
        exitButton.Click += OnExitClick;

        Controls.Add(exitButton);
        Controls.Add(scrambleGameButton);
        Controls.Add(typingGameButton);
        Controls.Add(crosswordButton);
        Controls.Add(titleLabel);
    }

    private void OnCrosswordClick(object sender, EventArgs e)
    {
        CrosswordForm crosswordGame = new CrosswordForm();
        crosswordGame.Show();
    }

    private void OnTypingGameClick(object sender, EventArgs e)
    {
        TypingGameForm typingGame = new TypingGameForm();
        typingGame.Show();
    }


    private void OnScrambleGameClick(object sender, EventArgs e)
    {
        ScrambleWordForm scrambleGame = new ScrambleWordForm();
        scrambleGame.Show();
        
    }

    private void OnExitClick(object sender, EventArgs e)
    {
        Application.Exit();
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new SpanishGamesForm());
    }
}
