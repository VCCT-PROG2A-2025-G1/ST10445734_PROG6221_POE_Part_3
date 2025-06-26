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
                { }
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
