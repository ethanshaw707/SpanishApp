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
            string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "pixel_horse.png");
            horseImage = new Bitmap(imagePath); // Load image from project directory
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading horse image: " + ex.Message);
            horseImage = new Bitmap(50, 50); // Fallback in case of error
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

        horsePictureBox = new PictureBox
        {
            Dock = DockStyle.Top,
            Height = 100,
            BackColor = Color.White,
            Image = horseImage,
            SizeMode = PictureBoxSizeMode.CenterImage
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
            stopwatch.Start(); // Start the stopwatch on the first key press
        }

        if (currentIndex < currentParagraph.Length && e.KeyChar == currentParagraph[currentIndex])
        {
            currentIndex++;
            instructionLabel.Text = currentParagraph.Substring(currentIndex);

            // Check if paragraph is completed
            if (currentIndex == currentParagraph.Length)
            {
                stopwatch.Stop(); // Stop the stopwatch
                double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                double wordsPerMinute = (currentParagraph.Split(' ').Length / elapsedSeconds) * 60;
                MessageBox.Show($"Congratulations! You've completed the paragraph!\nYour typing speed is {wordsPerMinute:F2} WPM.");
                DisplayNewParagraph();
                textBox.Clear();
            }
        }
        else
        {
            e.Handled = true; // Ignore incorrect key press
        }
    }
}