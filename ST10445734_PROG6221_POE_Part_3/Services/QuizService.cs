using ST10445734_PROG6221_POE_Part_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10445734_PROG6221_POE_Part_3.Services
{
    public class QuizService
    {
        private List<QuizQuestion> questions;
        private int currentQuestionIndex;
        public QuizResult Result { get; private set; }

        public QuizService()
        {
            LoadQuestions();
            Result = new QuizResult { TotalQuestions = questions.Count };
            currentQuestionIndex = 0;
        }

        private void LoadQuestions()
        {
            questions = new List<QuizQuestion>
            { new QuizQuestion
                {
                    Question = "What is the primary purpose of a firewall in cybersecurity?",
                    Options = new List<string> { "To block all internet traffic", "To monitor and control incoming and outgoing network traffic", "To encrypt data", "To create backups" },
                    CorrectAnswer = "To monitor and control incoming and outgoing network traffic",
                    Explanation = "A firewall acts as a barrier between a trusted internal network and untrusted external networks, controlling the flow of traffic based on predetermined security rules."
                },
                new QuizQuestion
                {
                    Question = "What does the acronym 'VPN' stand for?",
                    Options = new List<string> { "Virtual Private Network", "Virtual Public Network", "Variable Protocol Network", "Virtual Protected Network" },
                    CorrectAnswer = "Virtual Private Network",
                    Explanation = "A VPN creates a secure connection over the internet, allowing users to send and receive data as if they were directly connected to a private network."
                },
                new QuizQuestion
                {

                }
            };
        }

        public QuizQuestion GetCurrentQuestion()
        {
            if (currentQuestionIndex < questions.Count)
                return questions[currentQuestionIndex];
            return null;
        }

        public bool SubitAnswer(int selectedIndex, out string feedback)
        {
            var question = questions[selectedIndex];

            bool isCorrect = question.Options[selectedIndex] == question.CorrectAnswer;
            if (isCorrect)
            {
                Result.CorrectAnswers++;
                feedback = "Correct! " + question.Explanation;
            }
            else
            {
                feedback = "Incorrect. " + question.Explanation;
            }

            currentQuestionIndex++;
            return isCorrect;

        }

        public bool HasMoreQuestions()
        {
            return currentQuestionIndex < questions.Count;
        }

        public void NextQuestion()
        {
            if (HasMoreQuestions())
            {
                currentQuestionIndex++;
            }
        }

        public bool CheckAnswer(string selectedOption)
        {
            if (currentQuestionIndex < questions.Count)
            {
                var currentQuestion = questions[currentQuestionIndex];
                return currentQuestion.CorrectAnswer == selectedOption;
            }
            return false;
        }
    }
}
