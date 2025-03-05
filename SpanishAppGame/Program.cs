using System;
using System.Windows.Forms;

class Program
{
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new HelloWorldForm());
    }
}

public class HelloWorldForm : Form
{
    public HelloWorldForm()
    {
        Text = "Hello, World!";
        Width = 1600;
        Height = 1200;

        Label label = new Label();
        label.Text = "Hello, World!";
        label.Dock = DockStyle.Fill;
        label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        Controls.Add(label);
    }
}