using CyberSecurityBot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CyberSecurityBot
{
    class Program
    {
        //Cybersecurity awareness bot - main entry
        static void Main()
        {
            Console.Title = "Cybersecurity Awareness Bot";

            // Trigger audio welcome
            CyberSecurityBot.AudioPlayer.PlayGreeting();

            // Render visual header
            UIHelper.DisplayHeader();

            // Launch the assistant session
            ChatBot bot = new ChatBot();
            bot.StartChat();

            Console.ReadLine();
          

            //Part 2 WPF window
            Thread wpfThread = new Thread(() =>
            {
                var app = new Application();
                app.Run(new Part2.MainWindow());
            });
            wpfThread.SetApartmentState(ApartmentState.STA);
            wpfThread.Start();
        }
    }
}