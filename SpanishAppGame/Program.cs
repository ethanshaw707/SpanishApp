using System;
using System.Windows.Forms;

class Program
{
    [STAThread] // Required for Windows Forms applications
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Start the Typing Game Form
        Application.Run(new TypingGameForm()); // Updated from HelloWorldForm to TypingGameForm
    }
}
