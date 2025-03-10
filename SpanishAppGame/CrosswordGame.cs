namespace SpanishAppGame
{
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

public class CrosswordForm : Form{

    private TableLayoutPanel grid;
    private Dictionary<(int,int),(char letter, TextBox box)>crosswordGrid;
    private Dictionary<string, (List<(int, int)> positions, string hint)> words;
    private Label? hintLabel;

    private Button? submitButton;
    private Button? checkButton;
    private Panel? buttonPanel;

    public CrosswordForm()
        {
            Text = "Spanish Crossword Puzzle";
            Width = 400;
            Height = 600;

            grid = new TableLayoutPanel();
            crosswordGrid = new Dictionary<(int, int), (char, TextBox)>();
            words = new Dictionary<string, (List<(int, int)>, string)>
            {
                { "perro", (new List<(int, int)>(), "1. Animal that barks (English: Dog)") },
                { "gato", (new List<(int, int)>(), "2. A feline pet (English: Cat)") },
                { "casa", (new List<(int, int)>(), "3. A place where people live (English: House)") },
                { "sol", (new List<(int, int)>(), "4. The star that gives us light (English: Sun)") },
                { "luz", (new List<(int, int)>(), "5. What helps us see in darkness (English: Light)") },
                { "vino", (new List<(int, int)>(), "6. A drink made from grapes (English: Wine)") },
                { "pan", (new List<(int, int)>(), "7. A common breakfast food (English: Bread)") },
                { "agua", (new List<(int, int)>(), "8. Essential for life (English: Water)") },
                { "mesa", (new List<(int, int)>(), "9. You eat meals on this (English: Table)") },
                { "silla", (new List<(int, int)>(), "10. You sit on this (English: Chair)") }
                // { "perro", (new List<(int, int)> { (0,0), (0,1), (0,2), (0,3), (0,4) }, "1. Animal that barks (English: Dog)" ) },
                // { "gato", (new List<(int, int)> { (2,0), (2,1), (2,2), (2,3) }, "2. A feline pet (English: Cat)" ) },
                // { "casa", (new List<(int, int)> { (4,0), (4,1), (4,2), (4,3) }, "3. A place where people live (English: House)" ) }
            };

            PlaceWordsRandomly();
            InitializeGrid();
            CreateHintPanel();
            CreateButtons();
            }
        

        private void InitializeGrid()
        {
            grid = new TableLayoutPanel
            {
                RowCount = 6,
                ColumnCount = 6,
                Dock = DockStyle.Top,
                Width = 400,
                Height = 400,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            };

            for (int i = 0; i < 6; i++)
            {
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 16.66F));
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            }

            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 6; c++)
                {
                    TextBox box = new TextBox
                    {
                        Dock = DockStyle.Fill,
                        TextAlign = HorizontalAlignment.Center,
                        MaxLength = 1,
                        Font = new Font("Arial", 14),
                        BackColor = Color.LightGray, // Default color for unused cells
                        Enabled = false
                    };
                    box.GotFocus += OnBoxFocus;
                    grid.Controls.Add(box, c, r);
                }
            }

            // Add words to the grid
            foreach (var word in words)
            {
                foreach (var (row, col) in word.Value.positions)
                {
                    var box = (TextBox)grid.GetControlFromPosition(col, row);
                    box.BackColor = Color.White;
                    box.Enabled = true;
                    box.Tag = word.Key;
                    box.TextChanged += OnTextChanged;
                    crosswordGrid[(row, col)] = (word.Key[word.Value.positions.IndexOf((row, col))], box);
                }
            }

            Controls.Add(grid);
        }

         private void OnBoxFocus(object? sender, EventArgs e)
    {
        TextBox focusedBox = sender as TextBox;
        if (focusedBox != null && focusedBox.Tag is string word)
        {
            hintLabel.Text = words[word].hint;
        }
    }

    private void OnTextChanged(object? sender, EventArgs e)
    {
        TextBox box = sender as TextBox;
        if (box != null && box.Text.Length == 1)
        {
            var position = grid.GetPositionFromControl(box);
            int row = position.Row, col = position.Column;
            var nextPosition = crosswordGrid.Keys.FirstOrDefault(p => (p.Item1 == row && p.Item2 == col + 1) || (p.Item1 == row + 1 && p.Item2 == col));
            
            if (crosswordGrid.ContainsKey(nextPosition))
            {
                crosswordGrid[nextPosition].box.Focus();
            }
        }
    }

    private void PlaceWordsRandomly(){
        Random random = new Random();
        int gridSize = 6;

        foreach (var word in words.Keys.ToList()){
            bool placed = false;

            while (!placed){
                int row = random.Next(gridSize);
                int col = random.Next(gridSize);
                bool horizontal = random.Next(2) == 0;
                List<(int, int)> positions = new List<(int, int)>();

                if (horizontal && col + word.Length <= gridSize){
                    for (int i = 0; i < word.Length; i++){
                        positions.Add((row, col + i));
                    }
                }
                else if (!horizontal && row + word.Length <= gridSize){
                    for (int i = 0; i < word.Length ; i++){
                        positions.Add((row + i, col));
                    }
                }

                if (positions.Count == word.Length && !positions.Any(p => crosswordGrid.ContainsKey(p))){
                    words[word] = (positions, words[word].hint);
                    placed = true;
                }
            }
        }
    }

         private void CreateHintPanel()
        {
            hintLabel = new Label(); // Ensuring it's initialized
            hintLabel.Dock = DockStyle.Top;
            hintLabel.Text = "Hints will appear here.";
            hintLabel.TextAlign = ContentAlignment.MiddleCenter;
            hintLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            hintLabel.Height = 60;

            Panel hintPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70
            };

            hintPanel.Controls.Add(hintLabel);
            Controls.Add(hintPanel);
        }
        // {
        //     hintLabel = new Label
        //     {
        //         Dock = DockStyle.Top,
        //         Text = "Hints will appear here.",
        //         TextAlign = ContentAlignment.MiddleCenter,
        //         Font = new Font("Arial", 12, FontStyle.Bold),
        //         Height = 60
        //     };

        //     Panel hintPanel = new Panel
        //     {
        //         Dock = DockStyle.Top,
        //         Height = 70
        //     };

        //     hintPanel.Controls.Add(hintLabel);
        //     Controls.Add(hintPanel);
        // }

        private void CreateButtons()
        {
        buttonPanel = new Panel(); // Initialize before using
        buttonPanel.Dock = DockStyle.Bottom;
        buttonPanel.Height = 100;

        submitButton = new Button(); // Initialize
        submitButton.Text = "Show Hint";
        submitButton.Width = 100;
        submitButton.Height = 40;
        submitButton.Location = new Point(50, 20);
        submitButton.Click += OnShowHint;

        checkButton = new Button(); // Initialize
        checkButton.Text = "Check Answers";
        checkButton.Width = 120;
        checkButton.Height = 40;
        checkButton.Location = new Point(200, 20);
        checkButton.Click += OnCheckAnswers;

        buttonPanel.Controls.Add(submitButton);
        buttonPanel.Controls.Add(checkButton);
        Controls.Add(buttonPanel);
        }
        // {
        //     buttonPanel = new Panel
        //     {
        //         Dock = DockStyle.Bottom,
        //         Height = 100
        //     };

        //     submitButton = new Button
        //     {
        //         Text = "Show Hint",
        //         Width = 100,
        //         Height = 40,
        //         Location = new Point(50, 20)
        //     };
        //     submitButton.Click += OnShowHint;

        //     checkButton = new Button
        //     {
        //         Text = "Check Answers",
        //         Width = 120,
        //         Height = 40,
        //         Location = new Point(200, 20)
        //     };
        //     checkButton.Click += OnCheckAnswers;

        //     buttonPanel.Controls.Add(submitButton);
        //     buttonPanel.Controls.Add(checkButton);
        //     Controls.Add(buttonPanel);
        // }

        private void OnShowHint(object sender, EventArgs e)
        {
            hintLabel.Text = "Hints:\n" + string.Join("\n", words.Values.Select(v => $"➡ {v.hint}"));
        }

        private void OnCheckAnswers(object sender, EventArgs e)
        {
            bool allCorrect = true;
            foreach (var entry in crosswordGrid)
            {
                (char correctLetter, TextBox box) = entry.Value;
                if (box.Text.ToLower() == correctLetter.ToString())
                {
                    box.BackColor = Color.LightGreen;
                }
                else
                {
                    box.BackColor = Color.LightCoral;
                    allCorrect = false;
                }
            }

            if (allCorrect)
            {
                MessageBox.Show("✅ Congratulations! You completed the crossword!", "Success");
            }
        }
    }
}