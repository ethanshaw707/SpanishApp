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
    private Panel buttonPanel;

    public SpanishGamesForm()
    {
        Text = "Spanish Games";
        Width = 400;
        Height = 600;

        this.BackgroundImage = Image.FromFile("Resources/spanish-speaking-countries.jpg");
        this.BackgroundImageLayout = ImageLayout.Stretch;

         // Create a panel to center buttons
            buttonPanel = new Panel();
            buttonPanel.Size = new Size(300, 350); // Adjusted height for better layout
            buttonPanel.BackColor = Color.FromArgb(100, 255, 255, 255); // Semi-transparent background
            buttonPanel.Location = new Point((ClientSize.Width - buttonPanel.Width) / 2, (ClientSize.Height - buttonPanel.Height) / 2);
            buttonPanel.Anchor = AnchorStyles.None;

            // Title Label
            titleLabel = new Label();
            titleLabel.Text = "Welcome to the Spanish Games";
            titleLabel.Font = new Font("Comic Sans MS", 14, FontStyle.Bold);
            titleLabel.ForeColor = Color.Black;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Size = new Size(280, 50);
            titleLabel.Location = new Point(10, 10);

            // Create buttons
            crosswordButton = CreateStyledButton("1. Spanish Crossword", 70, OnCrosswordClick);
            typingGameButton = CreateStyledButton("2. Spanish Typing Game", 130, OnTypingGameClick);
            scrambleGameButton = CreateStyledButton("3. Spanish Scramble Game", 190, OnScrambleGameClick);
            flashcardBattleButton = CreateStyledButton("4. Flashcard Battle", 250, OnFlashcardBattleClick);
            exitButton = CreateStyledButton("5. Exit", 310, OnExitClick);
            exitButton.ForeColor = Color.White;
            exitButton.BackColor = Color.Red;

            // Add controls to panel
            buttonPanel.Controls.Add(titleLabel);
            buttonPanel.Controls.Add(crosswordButton);
            buttonPanel.Controls.Add(typingGameButton);
            buttonPanel.Controls.Add(scrambleGameButton);
            buttonPanel.Controls.Add(flashcardBattleButton);
            buttonPanel.Controls.Add(exitButton);

            // Add panel to the form
            Controls.Add(buttonPanel);
        }

        private Button CreateStyledButton(string text, int yOffset, EventHandler clickEvent)
        {
            Button button = new Button();
            button.Text = text;
            button.Font = new Font("Arial", 12, FontStyle.Bold);
            button.Size = new Size(250, 50);
            button.Location = new Point(25, yOffset); // Centering inside panel
            button.BackColor = Color.White; // Semi-transparent black
            button.ForeColor = Color.Black;
            button.FlatStyle = FlatStyle.Flat;
            button.Click += clickEvent;
            return button;
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