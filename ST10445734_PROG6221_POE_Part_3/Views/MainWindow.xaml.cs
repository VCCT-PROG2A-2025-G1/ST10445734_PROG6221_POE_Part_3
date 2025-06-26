using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
using ST10445734_PROG6221_POE_Part_3.Views;
using ST10445734_Prog6221_POE_Part1;
using Path = System.IO.Path;

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

            User user = new User(userName); // Create a new user object with the provided name

            // Show chat panel, hide user panel
            UserPanel.Visibility = Visibility.Collapsed;
            ChatDisplay2.Visibility = Visibility.Collapsed;
            ChatPanel.Visibility = Visibility.Visible;

            PlayWelcomeMessage(userName); // Play the welcome message with the user's name

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

        private static void PlayWelcomeMessage(string name) // Method to play welcoming message and display ASCII art header
        {

            // Get filename
            string fileName = "ElevenLabs_2025-03-02T18_19_54_Bill_pre_s83_sb75_se0_b_m2.wav";
            // Get the user profile dynamically
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile); // code from chatgpt to help dynamically use sound
            // Combine the user profile + filename and path folders dynamically
            string filePath = Path.Combine(userProfile, "source", "repos", "ST10445734_Prog6221_POE_Part_3", "ST10445734_Prog6221_POE_Part_3", fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    //Play Greeting audio with the heading
                    SoundPlayer snd = new SoundPlayer(filePath);
                    snd.PlaySync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while trying to play the sound: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    // if an error occurs while playing the sound, display an error message
                }
            }
            else
            {
                // else file not found
                MessageBox.Show("Welcome message file not found. Please ensure the file exists in the correct directory.", "File Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void OpenTaskWindow_Click(object sender, RoutedEventArgs e)
        {
            TaskWindow taskWindow = new TaskWindow();
            taskWindow.Show(); // Show the task window as a modal dialog
        }

        private void OpenQuizWindow_Click(object sender, RoutedEventArgs e)
        {
            QuizWindow quizWindow = new QuizWindow();
            quizWindow.Show(); // Show the quiz window as a modal dialog
        }
    }
}
