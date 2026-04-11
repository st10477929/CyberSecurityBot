using CyberSecurityBot.CyberSecurityBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityBot
{
    class Program
    {
        //Cybersecurity awareness bot - main entry
        static void Main()
        {
            Console.Title = "Cybersecurity Awareness Bot";

            // Trigger audio welcome
            AudioPlayer.PlayGreeting();

            // Render visual header
            UIHelper.DisplayHeader();

            // Launch the assistant session
            ChatBot bot = new ChatBot();
            bot.StartChat();

            Console.ReadLine();
        }
    }
}