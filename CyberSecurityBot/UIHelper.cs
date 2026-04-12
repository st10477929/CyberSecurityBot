using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace CyberSecurityBot
{
    internal class UIHelper
    
    {
        // Display ASCII art banner and styled header
        public static void DisplayHeader()
        
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("░█▀▀░█░█░█▀▄░█▀▀░█▀▄░█▀▀░█▀▀░█▀▀░█░█░█▀▄░▀█▀░▀█▀░█░█░░░█▀█░█░█░█▀█░█▀▄░█▀▀░█▀█░█▀▀░█▀▀░█▀▀░░░█▀▄░█▀█░▀█▀\r\n░█░░░░█░░█▀▄░█▀▀░█▀▄░▀▀█░█▀▀░█░░░█░█░█▀▄░░█░░░█░░░█░░░░█▀█░█▄█░█▀█░█▀▄░█▀▀░█░█░█▀▀░▀▀█░▀▀█░░░█▀▄░█░█░░█░\r\n░▀▀▀░░▀░░▀▀░░▀▀▀░▀░▀░▀▀▀░▀▀▀░▀▀▀░▀▀▀░▀░▀░▀▀▀░░▀░░░▀░░░░▀░▀░▀░▀░▀░▀░▀░▀░▀▀▀░▀░▀░▀▀▀░▀▀▀░▀▀▀░░░▀▀░░▀▀▀░░▀░");

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n**********************************************");
            Console.WriteLine("      [ CYBERSECURITY AWARENESS BOT ]        ");
            Console.WriteLine("**********************************************\n");
            Console.ResetColor();
        }

        public static void TypingEffect(string text)
        {
            foreach (char letter in text)
            {
                Console.Write(letter);
                Thread.Sleep(25);
            }
            Console.WriteLine();
        }
    }
}

