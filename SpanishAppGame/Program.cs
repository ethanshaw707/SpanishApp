using System;
using System.Windows.Forms;

class Program
{
    [STAThread] // Required for Windows Forms applications
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Show the HelloWorldForm first
        Application.Run(new SpanishGamesForm());
    }
}
