using System;
using System.Windows.Forms;
using SpanishAppGame;
namespace SpanishAppGame
{
public class SpanishGamesForm : Form
{
    private Button crosswordButton;
    private Button typingGameButton;
    private Button scrambleGameButton;
    private Button flashcardBattleButton;
    private Button exitButton;
    private Label titleLabel;

    public SpanishGamesForm()
    {
        Text = "Spanish Games";
        Width = 400;
        Height = 500;

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

        flashcardBattleButton = new Button();  // Button for Flashcard Battle
        flashcardBattleButton.Text = "4. Flashcard Battle";
        flashcardBattleButton.Dock = DockStyle.Top;
        flashcardBattleButton.Click += OnFlashcardBattleClick;

        exitButton = new Button();
        exitButton.Text = "5. Exit";
        exitButton.Dock = DockStyle.Top;
        exitButton.Click += OnExitClick;

        Controls.Add(exitButton);
        Controls.Add(flashcardBattleButton);
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
        MessageBox.Show("Typing Game (To be implemented)");
    }

    private void OnScrambleGameClick(object sender, EventArgs e)
    {
        ScrambleWordForm scrambleGame = new ScrambleWordForm();
        scrambleGame.Show();
        
    }
    private void OnFlashcardBattleClick(object sender, EventArgs e)  // New event for Flashcard Battle
    {
        FlashcardBattleForm flashcardBattleForm = new FlashcardBattleForm();
        flashcardBattleForm.Show(); // Open the Flashcard Battle form
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
}