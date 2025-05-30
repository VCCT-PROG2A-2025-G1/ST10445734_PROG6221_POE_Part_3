using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ST10445734_PROG6221_POE_Part_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string userName;
        public MainWindow()
        {
            InitializeComponent();
            ChatDisplay2.Text = "\r\n   _____      _                                        _ _          \r\n  / ____|    | |                                      (_) |         \r\n | |    _   _| |__   ___ _ __ ___  ___  ___ _   _ _ __ _| |_ _   _  \r\n | |   | | | | '_ \\ / _ \\ '__/ __|/ _ \\/ __| | | | '__| | __| | | | \r\n | |___| |_| | |_) |  __/ |  \\__ \\  __/ (__| |_| | |  | | |_| |_| | \r\n  \\_____\\__, |_.__/ \\___|_|  |___/\\___|\\___|\\__,_|_|  |_|\\__|\\__, | \r\n     /\\  __/ |                                                __/ | \r\n    /  \\|___/   ____ _ _ __ ___ _ __   ___  ___ ___          |___/  \r\n   / /\\ \\ \\ /\\ / / _` | '__/ _ \\ '_ \\ / _ \\/ __/ __|                \r\n  / ____ \\ V  V / (_| | | |  __/ | | |  __/\\__ \\__ \\                \r\n /_/__  \\_\\_/\\_/ \\__,_|_|  \\___|_| |_|\\___||___/___/                \r\n |  _ \\      | |                                                    \r\n | |_) | ___ | |_                                                   \r\n |  _ < / _ \\| __|                                                  \r\n | |_) | (_) | |_                                                   \r\n |____/ \\___/ \\__|                                                  \r\n                                                                    \r\n                                                                    \r\n";
        }

        private void StartChatbot_Click(object sender, RoutedEventArgs e)
        {
            userName = UserNameBox.Text.Trim();

            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Please enter your name.");
                return;
            }

            // Show chat panel, hide user panel
            UserPanel.Visibility = Visibility.Collapsed;
            ChatPanel.Visibility = Visibility.Visible;

            ChatOutput.Text = $"Hello {userName}, how can I assist you with cybersecurity today?";
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = UserInput.Text.Trim();

            if (!string.IsNullOrEmpty(userMessage))
            {
                ChatOutput.Text += $"\nYou: {userMessage}";
                // Here you would call your chatbot logic class
                // For example:
                // string response = chatbot.GetResponse(userMessage);
                // ChatOutput.Text += $"\nBot: {response}";

                UserInput.Clear();
            }
        }
    }
}
