using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;


public partial class FlashcardBattleForm : Form
{
    // Word pairs for the flashcard battle
    private static readonly Dictionary<string, string> wordPairs = new Dictionary<string, string>()
    {
        {"dog", "perro"},
        {"cat", "gato"},
        {"house", "casa"},
        {"book", "libro"},
        {"car", "coche"},
        {"water", "agua"},
        {"sun", "sol"},
        {"food", "comida"},
        {"friend", "amigo"},
        {"tree", "árbol"},
        {"computer", "computadora"},
        {"school", "escuela"},
        {"family", "familia"},
        {"music", "música"},
        {"movie", "película"},
        {"beach", "playa"},
        {"city", "ciudad"},
        {"work", "trabajo"},
        {"money", "dinero"},
        {"time", "tiempo"}
    };

    private static int player1Score = 0;
    private static int player2Score = 0;
    private static int currentRound = 1;
    private static Random random = new Random();
    private static int currentPlayer = 1;

    // UI Controls
    private Label player1ScoreLabel;
    private Label player2ScoreLabel;
    private TextBox answerTextBox;
    private Button startButton;
    private Button submitButton;
    private Label winnerLabel;
    private Label roundLabel;
    private Label wordLabel; // To display the word to translate
    private Label countdownLabel; // For countdown timer
    private Label playerLabel; // To display the current player
    private Stopwatch roundTimer;

    public FlashcardBattleForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.player1ScoreLabel = new Label();
        this.player2ScoreLabel = new Label();
        this.answerTextBox = new TextBox();
        this.startButton = new Button();
        this.submitButton = new Button();
        this.winnerLabel = new Label();
        this.roundLabel = new Label();
        this.wordLabel = new Label(); // Word display label
        this.countdownLabel = new Label(); // Countdown label
        this.playerLabel = new Label(); // Player label

        // Set up the labels and controls
        this.player1ScoreLabel.Text = "Player 1: 0";
        this.player1ScoreLabel.Location = new System.Drawing.Point(20, 20);
        this.Controls.Add(player1ScoreLabel);

        this.player2ScoreLabel.Text = "Player 2: 0";
        this.player2ScoreLabel.Location = new System.Drawing.Point(200, 20);
        this.Controls.Add(player2ScoreLabel);

        this.roundLabel.Text = "Round: 1";
        this.roundLabel.Location = new System.Drawing.Point(150, 50);
        this.Controls.Add(roundLabel);

        this.wordLabel.Text = "Word will appear here";
        this.wordLabel.Location = new System.Drawing.Point(150, 100);
        this.wordLabel.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
        this.Controls.Add(wordLabel);

        this.countdownLabel.Text = "3";
        this.countdownLabel.Location = new System.Drawing.Point(150, 140);
        this.countdownLabel.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);
        this.Controls.Add(countdownLabel);

        this.playerLabel.Text = "Player 1's Turn";
        this.playerLabel.Location = new System.Drawing.Point(150, 170); // Position it below the countdown
        this.Controls.Add(playerLabel);

        this.answerTextBox.Location = new System.Drawing.Point(20, 180);
        this.Controls.Add(answerTextBox);

        this.startButton.Text = "Start Game";
        this.startButton.Location = new System.Drawing.Point(20, 220);
        this.startButton.Click += new EventHandler(startButton_Click);
        this.Controls.Add(startButton);

        this.submitButton.Text = "Submit Answer";
        this.submitButton.Location = new System.Drawing.Point(200, 220);
        this.submitButton.Click += new EventHandler(submitButton_Click);
        this.Controls.Add(submitButton);

        this.winnerLabel.Location = new System.Drawing.Point(100, 250);
        this.Controls.Add(winnerLabel);

        // Handle Enter key press for submitting answer
        this.answerTextBox.KeyDown += new KeyEventHandler(answerTextBox_KeyDown);

        this.Text = "Flashcard Battle Game";
        this.Size = new System.Drawing.Size(400, 300);
    }

    // Start the game
    private void startButton_Click(object sender, EventArgs e)
    {
        player1Score = 0;
        player2Score = 0;
        currentRound = 1;
        currentPlayer = 1;
        player1ScoreLabel.Text = "Player 1: 0";
        player2ScoreLabel.Text = "Player 2: 0";
        winnerLabel.Text = "";
        roundLabel.Text = "Round: 1";

        answerTextBox.Enabled = false;
        submitButton.Enabled = false;

        startButton.Enabled = false; // Disable start button after game starts

        //PlayRound(currentPlayer);
        playerLabel.Text = "Player 1's Turn";
        StartCountdown(currentPlayer);  // Start first round for Player 1
    }

    // Handle answer submission when clicking submit button
    private void submitButton_Click(object sender, EventArgs e)
    {
        ProcessAnswerSubmission();
    }

    // Handle answer submission when pressing Enter key
    private void answerTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            ProcessAnswerSubmission();
        }
    }

    // Method to process the answer submission
    private void ProcessAnswerSubmission()
    {
        string playerAnswer = answerTextBox.Text.Trim().ToLower();
        string correctAnswer = wordPairs[wordLabel.Text];

        int points = 0;
        if (playerAnswer == correctAnswer)
        {
            points = CalculatePoints();
        }

        UpdateScore(points);
        answerTextBox.Clear();
        answerTextBox.Enabled = false; // Disable input until next round
        submitButton.Enabled = false;  // Disable submit until next round

        // Move to the next player's turn or to the next round
        if (currentPlayer == 1)
        {
            currentPlayer = 2;
            playerLabel.Text = "Player 2's Turn";
            StartCountdown(2); // Start countdown for Player 2
        }
        else
        {
            if (currentRound < 5)
            {
                currentRound++;
                roundLabel.Text = $"Round: {currentRound}";
                currentPlayer = 1;
                playerLabel.Text = "Player 1's Turn";
                StartCountdown(1); // Start countdown for Player 1 again
            }
            else
            {
                EndGame();
            }
        }
    }

    // Generate a random word for each round
    private void PlayRound(int playerNumber)
    {
        var wordList = new List<string>(wordPairs.Keys);
        string englishWord = wordList[random.Next(wordList.Count)];

        // Display the word to translate on the form
        wordLabel.Text = englishWord;

        // Start the round timer
        roundTimer = Stopwatch.StartNew();

        // Enable text box and submit button for the current player
        answerTextBox.Enabled = true;
        submitButton.Enabled = true;
        answerTextBox.Focus();
    }

    // Calculate points based on the time taken
    private int CalculatePoints()
    {
        double timeTaken = roundTimer.ElapsedMilliseconds / 1000.0;
        if (timeTaken < 2) return 10; // Super fast
        if (timeTaken < 4) return 7;  // Fast
        if (timeTaken < 6) return 5;  // Average speed
        return 3;                     // Slow but correct
    }

    // Update the score based on the points
    private void UpdateScore(int points)
    {
        if (currentPlayer == 1)
            player1Score += points;
        else
            player2Score += points;

        player1ScoreLabel.Text = $"Player 1: {player1Score}";
        player2ScoreLabel.Text = $"Player 2: {player2Score}";
    }

    // Start countdown before player's turn
    private void StartCountdown(int playerNumber)
{
    int countdownValue = 3; // Start countdown at 3 seconds
    countdownLabel.Text = $"{countdownValue}";

    System.Windows.Forms.Timer countdownTimer = new System.Windows.Forms.Timer(); // Explicitly specify the correct Timer class
    countdownTimer.Interval = 1000; // 1 second interval
    countdownTimer.Tick += (sender, e) =>
    {
        countdownValue--;
        countdownLabel.Text = $"{countdownValue}";
        if (countdownValue == 0)
        {
            countdownTimer.Stop();
            PlayRound(playerNumber); // Start the round after countdown
        }
    };
    countdownTimer.Start();
}

    // End the game and display the winner
    private void EndGame()
    {
        if (player1Score > player2Score)
        {
            winnerLabel.Text = "Player 1 Wins!";
        }
        else if (player2Score > player1Score)
        {
            winnerLabel.Text = "Player 2 Wins!";
        }
        else
        {
            winnerLabel.Text = "It's a Tie!";
        }

        // Disable further interaction
        answerTextBox.Enabled = false;
        submitButton.Enabled = false;
        startButton.Enabled = true; // Enable start button for a new game
    }
}
