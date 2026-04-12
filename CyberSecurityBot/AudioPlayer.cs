using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace CyberSecurityBot
{
    using System;
    using System.Media;
    using System.IO;

    namespace CyberSecurityBot
    {
        // Handles all audio playback functionality 
        public static class AudioPlayer
        {
            public static void PlayGreeting()
            {
                try
                {
                    string audioPath = Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Assets",
                        "greeting.wav"
                    );

                    if (File.Exists(audioPath))
                    {
                        SoundPlayer soundPlayer = new SoundPlayer(audioPath);
                        soundPlayer.PlaySync();
                    }
                    else
                    {
                        Console.Beep(600, 400);
                        Console.Beep(800, 400);
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine("[!] Voice playback error: " + ex.Message);
                }
            }
        }
    }
}