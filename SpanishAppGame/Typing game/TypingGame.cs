using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

public class TypingGameForm : Form
{
    private Button startButton;
    private TextBox displayTextBox;
    private TextBox inputTextBox;
    private int horseStartX = 10;  // Starting position (left side)
    private int horseFinishX;      // Finish line position

    private PictureBox playerHorse;
    private PictureBox npcHorse1, npcHorse2, npcHorse3;
    private Bitmap horseImage;
    private List<string> sentences;
    private string currentParagraph = "";
    private int currentIndex;
    private Stopwatch stopwatch;
    private System.Windows.Forms.Timer raceTimer; // Specify the Timer type
    private Random rng;
    private int errorCount; // Add this field to count errors
    private Label countdownLabel; // Add this field for countdown

    public TypingGameForm()
    {
        Text = "Typing Game with Horse";
        Width = 800;
        Height = 400;

        startButton = new Button
        {
            Text = "Start Typing Game",
            Dock = DockStyle.Fill
        };
        startButton.Click += new EventHandler(OnStartButtonClick);
        Controls.Add(startButton);

        sentences = new List<string>
        {
            "El rapido zorro marron salta sobre el perro perezoso.",
            "Aprender a programar es divertido y gratificante.",
            "Un viaje de mil millas comienza con un solo paso.",
            "La practica hace al maestro, asi que sigue escribiendo.",
            "Depurar es como ser un detective en una pelicula de crimen."
        };

        try
        {
            string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", "pixel_horse.png");
            if (!System.IO.File.Exists(imagePath))
            {
                MessageBox.Show("Image file not found at: " + imagePath);
            }
            else
            {
                Bitmap originalImage = new Bitmap(imagePath);
                horseImage = new Bitmap(originalImage, new Size(50, 50));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading horse image: " + ex.Message);
            horseImage = new Bitmap(50, 50);
        }

        stopwatch = new Stopwatch();
        rng = new Random();
        raceTimer = new System.Windows.Forms.Timer(); // Specify the Timer type
        raceTimer.Interval = 50; // 50 ms interval for smoother and slower movement
        raceTimer.Tick += RaceTimer_Tick;
    }

    private void OnStartButtonClick(object sender, EventArgs e)
    {
        Controls.Remove(startButton);

        displayTextBox = new TextBox
        {
            Dock = DockStyle.Top,
            Font = new Font("Arial", 16),
            ReadOnly = true,
            Multiline = true,
            Height = 100,
            Width = this.ClientSize.Width,
            Text = currentParagraph
        };

        inputTextBox = new TextBox
        {
            Dock = DockStyle.Top,
            Font = new Font("Arial", 16)
        };
        inputTextBox.KeyPress += new KeyPressEventHandler(OnKeyPress);

        horseFinishX = this.ClientSize.Width - 100;

        Label finishLine = new Label
        {
            Width = 5,
            Height = 200,
            BackColor = Color.Black,
            Location = new Point(horseFinishX, 150)
        };
        Controls.Add(finishLine);

        playerHorse = new PictureBox
        {
            Width = 50,
            Height = 50,
            BackColor = Color.Transparent,
            Image = ApplyColorFilter(horseImage, Color.Red), // Apply color filter to player's horse
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(horseStartX, 250)
        };

        npcHorse1 = new PictureBox
        {
            Width = 50,
            Height = 50,
            BackColor = Color.Transparent,
            Image = horseImage,
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(horseStartX, 150)
        };

        npcHorse2 = new PictureBox
        {
            Width = 50,
            Height = 50,
            BackColor = Color.Transparent,
            Image = horseImage,
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(horseStartX, 200)
        };

        npcHorse3 = new PictureBox
        {
            Width = 50,
            Height = 50,
            BackColor = Color.Transparent,
            Image = horseImage,
            SizeMode = PictureBoxSizeMode.Zoom,
            Location = new Point(horseStartX, 300)
        };

        Controls.Add(playerHorse);
        Controls.Add(npcHorse1);
        Controls.Add(npcHorse2);
        Controls.Add(npcHorse3);
        Controls.Add(inputTextBox);
        Controls.Add(displayTextBox);

        DisplayNewParagraph();
        StartCountdown(); // Start the countdown before the race
    }

    private Bitmap ApplyColorFilter(Bitmap originalImage, Color color)
    {
        Bitmap newImage = new Bitmap(originalImage.Width, originalImage.Height);
        using (Graphics g = Graphics.FromImage(newImage))
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {0.4f, 0.2f, 0.1f, 0, 0},
                new float[] {0.3f, 0.2f, 0.1f, 0, 0},
                new float[] {0.2f, 0.1f, 0.1f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(originalImage, new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, attributes);
        }
        return newImage;
    }

    private void DisplayNewParagraph()
    {
        Random random = new Random();
        currentParagraph = string.Join(" ", sentences.OrderBy(x => random.Next()).Take(4));
        currentIndex = 0;
        displayTextBox.Text = currentParagraph;
        stopwatch.Reset();
        errorCount = 0; // Initialize error count
    }

    private void OnKeyPress(object sender, KeyPressEventArgs e)
    {
        if (currentIndex == 0 && !stopwatch.IsRunning)
        {
            stopwatch.Start();
        }

        if (currentIndex < currentParagraph.Length && e.KeyChar == currentParagraph[currentIndex])
        {
            currentIndex++;
            displayTextBox.Text = currentParagraph.Substring(currentIndex);

            int progress = (int)((double)currentIndex / currentParagraph.Length * (horseFinishX - horseStartX));
            playerHorse.Location = new Point(horseStartX + progress, playerHorse.Location.Y);

            if (currentIndex == currentParagraph.Length)
            {
                stopwatch.Stop();
                raceTimer.Stop(); // Stop the race timer
                double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                double wordsPerMinute = (currentParagraph.Split(' ').Length / elapsedSeconds) * 60;
                double accuracy = ((double)(currentParagraph.Length - errorCount) / currentParagraph.Length) * 100;
                string position = DeterminePlayerPosition();
                MessageBox.Show($"Congratulations! You've completed the paragraph!\nYour typing speed is {wordsPerMinute:F2} WPM.\nYour accuracy is {accuracy:F2}%.\nYou finished in {position} place.");
                ResetGame();
            }
        }
        else
        {
            errorCount++; // Increment error count
            e.Handled = true;
        }
    }

    private string DeterminePlayerPosition()
    {
        int playerX = playerHorse.Location.X;
        int[] positions = { npcHorse1.Location.X, npcHorse2.Location.X, npcHorse3.Location.X, playerX };
        Array.Sort(positions);
        Array.Reverse(positions); // Reverse the array to get the correct position
        int playerPosition = Array.IndexOf(positions, playerX) + 1;
        return playerPosition.ToString();
    }

    private void RaceTimer_Tick(object sender, EventArgs e)
    {
        MoveHorse(npcHorse1, 0.42); // Slow speed
        MoveHorse(npcHorse2, 0.45); // Medium speed
        MoveHorse(npcHorse3, 0.5); // Fast speed
    }

    private void MoveHorse(PictureBox horse, double speed)
    {
        double randomStep = rng.NextDouble() * speed - 0.1; // Random step size for NPC horses, allowing decimals
        horse.Location = new Point(horse.Location.X + (int)(randomStep * 10), horse.Location.Y);
        if (horse.Location.X >= horseFinishX)
        {
            horse.Location = new Point(horseFinishX, horse.Location.Y);
        }
    }

    private void ResetGame()
    {
        inputTextBox.Clear();
        currentIndex = 0;
        errorCount = 0; // Reset error count
        playerHorse.Location = new Point(horseStartX, playerHorse.Location.Y);
        npcHorse1.Location = new Point(horseStartX, npcHorse1.Location.Y);
        npcHorse2.Location = new Point(horseStartX, npcHorse2.Location.Y);
        npcHorse3.Location = new Point(horseStartX, npcHorse3.Location.Y);
        DisplayNewParagraph();
        StartCountdown(); // Restart the countdown before the race
    }

    private void StartCountdown()
    {
        countdownLabel = new Label
        {
            Font = new Font("Arial", 48),
            Size = new Size(200, 100),
            Location = new Point((ClientSize.Width - 200) / 2, (ClientSize.Height - 100) / 2),
            TextAlign = ContentAlignment.MiddleCenter
        };
        Controls.Add(countdownLabel);
        Countdown(3);
    }

    private async void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            countdownLabel.Text = i.ToString();
            await Task.Delay(1000);
        }
        countdownLabel.Text = "Go!";
        await Task.Delay(1000);
        Controls.Remove(countdownLabel);
        raceTimer.Start(); // Start the race timer
    }
}