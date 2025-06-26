using ST10445734_PROG6221_POE_Part_3.Services;
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
using ST10445734_PROG6221_POE_Part_3.Models;

namespace ST10445734_PROG6221_POE_Part_3.Views
{
    /// <summary>
    /// Interaction logic for QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        private QuizService quizService;
        private List<RadioButton> optionButtons;
        public QuizWindow()
        {
            InitializeComponent();
            quizService = new QuizService();
            optionButtons = new List<RadioButton>();
            LoadQuestion();
        }

        private void LoadQuestion()
        {
            if (quizService.HasMoreQuestions())
            {
                QuizQuestion current = quizService.GetCurrentQuestion();
                QuestionTextBlock.Text = current.Question;
                FeedbackTextBlock.Visibility = Visibility.Collapsed;
                SubmitButton.Content = "Submit Answer";

                OptionsPanel.Children.Clear();
                optionButtons.Clear();

                foreach (string option in current.Options)
                {
                    RadioButton radioB = new RadioButton
                    {
                        Content = option,
                        Margin = new Thickness(0, 5, 0, 5),
                        GroupName = "OptionsGroup"
                    };
                    OptionsPanel.Children.Add(radioB);
                    optionButtons.Add(radioB);
                }
            }
            else 
            {
                ShowFinalResults();
            }
        }

        private void ShowFinalResults()
        {
            int score = quizService.Result.CorrectAnswers;
            int total = quizService.Result.TotalQuestions;

            MessageBox.Show($"Quiz Completed!\nYou answered {score} out of {total} questions correctly.\n\n{quizService.Result.Feedback}", "Quiz Results", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubmitButton.Content.ToString() == "Next Question") 
            {
                quizService.NextQuestion();
                LoadQuestion();
                return;
            }

            string selectedOption = null;
            foreach (RadioButton radBut in optionButtons) 
            {
                if (radBut.IsChecked == true) 
                {
                    selectedOption = radBut.Content.ToString();
                    break;
                }
            }

            if (selectedOption == null)
            {
                MessageBox.Show("Please select an answer before submitting.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            bool isCorrect = quizService.CheckAnswer(selectedOption);
            string feedback = isCorrect ? "Correct!!!" : "Incorrect!!!";
            feedback += quizService.GetCurrentQuestion().Explanation;

            FeedbackTextBlock.Text = feedback;
            FeedbackTextBlock.Visibility = Visibility.Visible;
            SubmitButton.Content = "Next Question";

        }

    }
}
