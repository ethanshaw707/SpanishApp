using System;
using System.Windows.Forms;
using SpanishAppGame;

class Program
{
    [STAThread] // Required for Windows Forms applications
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Show the SpanishGamesForm first
        Application.Run(new SpanishGamesForm());
    }
}
