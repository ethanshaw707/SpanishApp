using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class TypingGameForm : Form
{
    private Button startButton;
    private TextBox textBox;
    private Label instructionLabel;
    private int horseStartX = 10;  // Starting position (left side)
    private int horseFinishX;      // Finish line position

    private PictureBox horsePictureBox;
    private Bitmap horseImage;
    private List<string> sentences;
    private string currentParagraph = "";
    private int currentIndex;
    private Stopwatch stopwatch;

    public TypingGameForm()
    {
        Text = "Typing Game with Horse";
        Width = 800;
        Height = 300;

        startButton = new Button
        {
            Text = "Start Typing Game",
            Dock = DockStyle.Fill
        };
        startButton.Click += new EventHandler(OnStartButtonClick);
        Controls.Add(startButton);

        // Predefined sentences
        sentences = new List<string>
        {
            "El rapido zorro marron salta sobre el perro perezoso.",
            "Aprender a programar es divertido y gratificante.",
            "Un viaje de mil millas comienza con un solo paso.",
            "La practica hace al maestro, asi que sigue escribiendo.",
            "Depurar es como ser un detective en una pelicula de crimen."
        };

        // Load horse image correctly
        try
        {
            string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", "pixel_horse.png");
            MessageBox.Show($"Looking for image at: {imagePath}");

            if (!System.IO.File.Exists(imagePath))
            {
                MessageBox.Show("Image file not found at: " + imagePath);
            }
            else
            {
                Bitmap originalImage = new Bitmap(imagePath);
                
                // Resize the image to make it smaller
                int newWidth = 50;  
                int newHeight = 50; 

                horseImage = new Bitmap(originalImage, new Size(newWidth, newHeight));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading horse image: " + ex.Message);
            horseImage = new Bitmap(50, 50);
        }

        stopwatch = new Stopwatch();
    }

    private void OnStartButtonClick(object sender, EventArgs e)
    {
        Controls.Remove(startButton);

        instructionLabel = new Label
        {
            Dock = DockStyle.Top,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 16)
        };

        textBox = new TextBox
        {
            Dock = DockStyle.Top,
            Font = new Font("Arial", 16)
        };
        textBox.KeyPress += new KeyPressEventHandler(OnKeyPress);

        // Set finish line position before adding it
        horseFinishX = this.ClientSize.Width - 100; 

        Label finishLine = new Label
        {
            Width = 5,
            Height = 100,
            BackColor = Color.Black,
            Location = new Point(horseFinishX, 130)
        };
        Controls.Add(finishLine);

horsePictureBox = new PictureBox
{
    Width = 50,  // Fixed width
    Height = 50, // Fixed height
    BackColor = Color.Transparent,
    Image = horseImage,
    SizeMode = PictureBoxSizeMode.Zoom, // Ensures aspect ratio is maintained
    Location = new Point(horseStartX, 150)
};


        Controls.Add(horsePictureBox);
        Controls.Add(textBox);
        Controls.Add(instructionLabel);

        // Display the first paragraph
        DisplayNewParagraph();
    }

    private void DisplayNewParagraph()
    {
        Random random = new Random();
        currentParagraph = string.Join(" ", sentences.OrderBy(x => random.Next()).Take(4));
        currentIndex = 0;
        instructionLabel.Text = currentParagraph;
        stopwatch.Reset(); // Reset the stopwatch
    }

    private void OnKeyPress(object sender, KeyPressEventArgs e)
    {
        if (currentIndex == 0 && !stopwatch.IsRunning)
        {
            stopwatch.Start(); // Start stopwatch on first key press
        }

        if (currentIndex < currentParagraph.Length && e.KeyChar == currentParagraph[currentIndex])
        {
            currentIndex++;
            instructionLabel.Text = currentParagraph.Substring(currentIndex);

            // Move horse forward based on progress
            int progress = (int)((double)currentIndex / currentParagraph.Length * (horseFinishX - horseStartX));
            horsePictureBox.Location = new Point(horseStartX + progress, horsePictureBox.Location.Y);

            // Check if paragraph is completed
            if (currentIndex == currentParagraph.Length)
            {
                stopwatch.Stop();
                double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                double wordsPerMinute = (currentParagraph.Split(' ').Length / elapsedSeconds) * 60;
                MessageBox.Show($"Congratulations! You've completed the paragraph!\nYour typing speed is {wordsPerMinute:F2} WPM.");
                ResetGame();
            }
        }
        else
        {
            e.Handled = true; // Ignore incorrect key press
        }
    }

    private void ResetGame()
    {
        textBox.Clear();
        currentIndex = 0;
        horsePictureBox.Location = new Point(horseStartX, horsePictureBox.Location.Y); // Reset horse position
        DisplayNewParagraph();
    }
}
