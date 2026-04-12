using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityBot
{
    internal class ChatBot
    {
        private string userName;

        public void StartChat()
        {
            AskName();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nYou: ");
                Console.ResetColor();

                string userInput = Console.ReadLine()?.ToLower().Trim();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Respond("That seemed empty — could you try again?");
                    continue;
                }

                if (userInput.Contains("exit") || userInput.Contains("bye"))
                {
                    Respond($"Take care, {userName}! Stay secure out there. 🛡️");
                    break;
                }

                HandleResponse(userInput);
            }
        }

       //Greets user and collects their name
        private void AskName()
        {
            Console.Write("What's your name? ");
            userName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(userName))
                userName = "Guest";

            Respond($"Welcome aboard, {userName}! I'm your cybersecurity awareness assistant.");
        }

        private void HandleResponse(string userInput)
        {
            if (userInput.Contains("how are you") || userInput.Contains("doing"))
            {
                Respond("Running at full capacity and ready to keep you secure!");
            }
            else if (userInput.Contains("purpose") || userInput.Contains("what do you do"))
            {
                Respond("I help you understand cybersecurity threats and how to stay protected online.");
            }
            else if (userInput.Contains("ask") || userInput.Contains("help") || userInput.Contains("topics"))
            {
                Respond("Try asking me about: passwords, phishing scams, or safe browsing habits.");
            }
            else if (userInput.Contains("password"))
            {
                Respond("Strong passwords mix uppercase, lowercase, numbers and symbols. Never reuse them across accounts!");
            }
            else if (userInput.Contains("phishing"))
            {
                Respond("Phishing attacks disguise themselves as trusted sources. Always double-check email senders and links.");
            }
            else if (userInput.Contains("browsing") || userInput.Contains("internet"))
            {
                Respond("Stick to HTTPS sites, avoid sketchy downloads, and consider a VPN on public Wi-Fi.");
            }
            else
            {
                Respond("I didn't quite understand that. Could you rephrase?");
            }
        }

        private void Respond(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            UIHelper.TypingEffect("Bot: " + message);
            Console.ResetColor();
        }
    }
}
    
