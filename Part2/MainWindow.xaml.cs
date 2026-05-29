using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Part2
{
    public partial class MainWindow : Window
    {
        // ── 1. Keyword Responses Dictionary ──────────────────────────────────
        Dictionary<string, string> responses = new Dictionary<string, string>()
        {
            {"hello",           "Hello there! How can I help you stay safe online today?"},
            {"how are you",     "I'm doing well, thank you! Ready to help with cybersecurity tips."},
            {"what is wpf",     "WPF stands for Windows Presentation Foundation — a desktop application framework."},
            {"what is c#",      "C# is a modern, object-oriented programming language developed by Microsoft."},
            {"what is sql",     "SQL stands for Structured Query Language, used to manage databases."},

            // ── 2. Keyword Recognition (cybersecurity) ──────────────────────
            {"password",        "Use strong, unique passwords for each account. Avoid using personal details in your passwords. Consider using a password manager!"},
            {"scam",            "Be cautious of suspicious emails or messages asking for personal information. Verify the sender before responding."},
            {"privacy",         "Review your account security settings regularly to protect your privacy online. Limit the personal information you share."},
            {"phishing",        "Phishing is when attackers disguise themselves as trusted sources. Never click suspicious links and always verify email senders."},
            {"malware",         "Malware is malicious software designed to harm your device. Keep your antivirus updated and avoid downloading from unknown sources."},
            {"firewall",        "A firewall monitors and controls network traffic to protect your system. Always keep it enabled on your device."},
            {"encryption",      "Encryption protects your data by converting it into an unreadable format. Use encrypted connections (HTTPS) when browsing."},
            {"vpn",             "A VPN (Virtual Private Network) secures your internet connection. It's especially useful on public Wi-Fi networks."},
            {"hacking",         "Hacking involves unauthorised access to systems. Protect yourself with strong passwords, 2FA, and regular software updates."},
            {"2fa",             "Two-Factor Authentication adds an extra layer of security to your accounts. Enable it wherever possible!"},
        };

        // ── 3. Random Responses (phishing tips list) ─────────────────────────
        List<string> phishingTips = new List<string>()
        {
            "Be cautious of emails asking for personal information.",
            "Scammers often disguise themselves as trusted organisations.",
            "Never click suspicious links in emails or messages.",
            "Verify the sender's email address before responding.",
            "Legitimate companies will never ask for your password via email.",
            "Check for spelling mistakes — phishing emails often contain errors."
        };

        // ── 5. Memory & Recall ───────────────────────────────────────────────
        Dictionary<string, string> memory = new Dictionary<string, string>();
        private string userName = "";
        private bool nameStored = false;

        // ── 6. Sentiment Detection ───────────────────────────────────────────
        Dictionary<string, string> sentiments = new Dictionary<string, string>()
        {
            {"worried",     "It's completely understandable to feel that way. Scammers can be very convincing. Let me share some tips to help you stay safe."},
            {"scared",      "Don't worry — knowing about cybersecurity threats already puts you ahead! Let me help you feel more confident."},
            {"curious",     "Great curiosity! Learning about cybersecurity is a very smart move. What would you like to know?"},
            {"frustrated",  "I understand your frustration. Let me simplify this for you — cybersecurity doesn't have to be overwhelming."},
            {"confused",    "No problem at all! Let me explain that in a simpler way for you."},
            {"angry",       "I hear you. Let me help resolve this cybersecurity concern as clearly as possible."}
        };

        // ── Conversation flow state ──────────────────────────────────────────
        private string lastTopic = "";
        private Random rnd = new Random();

        // ────────────────────────────────────────────────────────────────────
        public MainWindow()
        {
            InitializeComponent();
            AppendMessage("CyberBot", "Hello! I'm CyberBot 🤖. What's your name?", Brushes.DarkGreen);
        }

        // ── Send button click ────────────────────────────────────────────────
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = userInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AppendMessage("You", input, Brushes.DarkBlue);
            userInput.Clear();

            string response = GetResponse(input);
            AppendMessage("CyberBot", response, Brushes.DarkGreen);
        }

        // ── Enter key support ────────────────────────────────────────────────
        private void UserInputBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                SendButton_Click(sender, e);
        }

        // ── Core response logic ──────────────────────────────────────────────
        private string GetResponse(string input)
        {
            string lower = input.ToLower().Trim();

            // ── Collect name first ───────────────────────────────────────────
            if (!nameStored)
            {
                userName = input;
                nameStored = true;
                memory["name"] = userName;
                return $"Nice to meet you, {userName}! 😊 I'm here to help you stay safe online. " +
                       "Ask me about passwords, phishing, scams, privacy, malware, VPNs, and more!";
            }

            // ── 4. Conversation flow: follow-up triggers ─────────────────────
            if ((lower.Contains("more") || lower.Contains("another") ||
                 lower.Contains("tell me more") || lower.Contains("explain more")) &&
                !string.IsNullOrEmpty(lastTopic))
            {
                return GetTopicResponse(lastTopic);
            }

            // ── 6. Sentiment detection ───────────────────────────────────────
            foreach (var sentiment in sentiments)
            {
                if (lower.Contains(sentiment.Key))
                {
                    // After empathy, share a tip automatically (no re-prompting)
                    string tip = phishingTips[rnd.Next(phishingTips.Count)];
                    return $"{sentiment.Value}\n\n💡 Quick tip: {tip}";
                }
            }

            // ── 5. Memory: store user interests ─────────────────────────────
            if (lower.Contains("interested in") || lower.Contains("i like") || lower.Contains("i love"))
            {
                foreach (var key in responses.Keys)
                {
                    if (lower.Contains(key))
                    {
                        memory["interest"] = key;
                        lastTopic = key;
                        return $"Great! I'll remember that you're interested in {key}. " +
                               $"{responses[key]}";
                    }
                }
            }

            // ── Recall stored interest ───────────────────────────────────────
            if (lower.Contains("remember") || lower.Contains("what do you know about me"))
            {
                if (memory.ContainsKey("interest"))
                    return $"I remember you're interested in {memory["interest"]}, {userName}. " +
                           $"As someone interested in {memory["interest"]}, you should {responses[memory["interest"]]}";
                return $"I know your name is {userName}. Tell me more about your cybersecurity interests!";
            }

            // ── 3. Random responses for phishing ─────────────────────────────
            if (lower.Contains("phishing tip") || lower.Contains("give me a tip"))
            {
                lastTopic = "phishing";
                return $"💡 Phishing Tip: {phishingTips[rnd.Next(phishingTips.Count)]}";
            }

            // ── 2. Keyword recognition ────────────────────────────────────────
            foreach (var keyword in responses.Keys)
            {
                if (lower.Contains(keyword))
                {
                    lastTopic = keyword;

                    // Personalise with name if available
                    string reply = responses[keyword];
                    if (memory.ContainsKey("interest") && memory["interest"] == keyword)
                        reply += $"\n\nSince you're interested in {keyword}, {userName}, this is especially important for you!";

                    return reply;
                }
            }

            // ── Greet by name ─────────────────────────────────────────────────
            if (lower.Contains("hi") || lower.Contains("hey") || lower.Contains("good"))
                return $"Hey {userName}! 👋 How can I help you with cybersecurity today?";

            // ── 7. Error handling / default response ─────────────────────────
            return $"I'm not sure I understand that, {userName}. Can you try rephrasing? " +
                   "You can ask me about: passwords, phishing, scams, privacy, malware, VPNs, encryption, firewalls, or 2FA.";
        }

        // ── Helper: get another response for the same topic ──────────────────
        private string GetTopicResponse(string topic)
        {
            if (topic == "phishing")
                return $"💡 Another phishing tip: {phishingTips[rnd.Next(phishingTips.Count)]}";

            if (responses.ContainsKey(topic))
                return $"Here's more about {topic}: {responses[topic]}\n\n" +
                       $"💡 Bonus tip: {phishingTips[rnd.Next(phishingTips.Count)]}";

            return "I don't have more details on that topic right now. Try asking about something else!";
        }

        // ── Append message to chat display ───────────────────────────────────
        private void AppendMessage(string sender, string message, Brush colour)
        {
            TextBlock tb = new TextBlock
            {
                Text = $"{sender}: {message}",
                Foreground = colour,
                FontSize = 14,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5, 3, 5, 3)
            };
            ChatPanel.Children.Add(tb);
            ChatScrollViewer.ScrollToBottom();
        }
    }
}