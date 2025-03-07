using System;
using System.Windows.Forms;

class Program
{
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Show the HelloWorldForm first
        Application.Run(new HelloWorldForm());
    }
}