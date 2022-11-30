using Newtonsoft.Json;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;
using System.Text.Unicode;
using System.Text;


namespace NotepadHarasser
{
    struct Character
    {
        public string character;
        public long timeSpan;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Nu ska vi trakassera notepad lite!!");

            string path = "result.json";
            string JSON = File.ReadAllText(path);

            List<Character>? characters = JsonConvert.DeserializeObject<List<Character>>(JSON);

            if (characters != null)
            {
                /*foreach (Character c in characters)
                {
                    if (c.character == '\r')
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write(c.character);
                    }

                    Thread.Sleep((int)c.timeSpan);
                }*/

                HarrassNotepad(characters);
            }
        }

        static void HarrassNotepad(List<Character> characters)
        {
            FlaUI.Core.Application app = Application.Launch("notepad.exe");

            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                System.Diagnostics.Debug.WriteLine(window.Title);
                //Console.WriteLine(window.Title);

                var children = window.FindAllChildren();
                foreach (var child in children)
                {
                    System.Diagnostics.Debug.WriteLine(child);

                    if (child.Name == "Text Editor")
                    {
                        var textBox = child.AsTextBox();
                        PrintIntoTextBox(textBox, characters);
                    }
                }
            }
        }

        static void PrintIntoTextBox(TextBox textBox, List<Character> characters)
        {
            string theText = "";
            int length = characters.Count;
            int pos = 0;

            foreach (Character c in characters)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write($"Writing character nr {pos} out of {length} characters.");
                pos++;

                theText += c.character;

                if (c.character == "\r")
                {
                    textBox.Text += Environment.NewLine;
                }
                else if (c.character == "\b")
                {
                    textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                }
                else
                {
                    textBox.Text += c.character;

                    // This places the cursor correct, but everything blinks.
                    //textBox.Enter(theText);
                }

                Thread.Sleep((int)c.timeSpan);
            }

            //textBox.Text = "Tomten!";
            //textBox.RightClick();
        }
    }
}