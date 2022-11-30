using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace KeyboardReader2
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
            List<Character> characters = new();

            Console.WriteLine("Hello, World!");

            string res = "";
            Stopwatch stopwatch = new();
            char daLetter;

            while(true)
            {
                stopwatch.Restart();
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                daLetter = consoleKeyInfo.KeyChar;
                stopwatch.Stop();
                long ms = stopwatch.ElapsedMilliseconds;

                if(daLetter == '+')
                {
                    break;
                }

                byte[] theCharacter = { (byte)daLetter };
                string theChar = Encoding.UTF8.GetString(theCharacter);

                // För jag orkar inte! :-)
                if (daLetter == 'å')
                {
                    theChar = "å";
                }
                else if (daLetter == 'ä')
                {
                    theChar = "ä";
                }
                else if (daLetter == 'ö')
                {
                    theChar = "ö";
                }
                else if (daLetter == 'Å')
                {
                    theChar = "Å";
                }
                else if (daLetter == 'Ä')
                {
                    theChar = "Ä";
                }
                else if (daLetter == 'Ö')
                {
                    theChar = "Ö";
                }

                Character character = new()
                {
                    timeSpan = ms,
                    character = theChar
                };

                characters.Add(character);

                if (daLetter == '\b')
                {
                    res = res.Substring(0, res.Length - 1);
                }
                else
                {
                    res += daLetter;
                }
            } 

            Console.WriteLine();
            Console.WriteLine();

            if (false)
            {
                foreach (Character c in characters)
                {
                    if (c.character == "\r")
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write(c.character);
                    }

                    Thread.Sleep((int)c.timeSpan);
                }
            }

            string JSON = JsonConvert.SerializeObject(characters);

            string path = "result.json";
            File.WriteAllText(path, JSON);

            Console.WriteLine();
            Console.WriteLine("Doonnee");
        }
    }
}