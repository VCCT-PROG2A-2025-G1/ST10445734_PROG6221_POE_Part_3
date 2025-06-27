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
                Question = "What is phishing?",
                Options = new List<string>
                {
                    "A way to catch fish using the internet",
                    "An attempt to obtain sensitive information by pretending to be a trustworthy entity",
                    "A software update",
                    "A type of firewall"
                },
                CorrectAnswerIndex = 1,
                Explanation = "Phishing is a social engineering attack often used to steal user data, such as login credentials or credit card numbers."
            },
            new QuizQuestion
            {
                Question = "True or False: You should use the same password for all your online accounts.",
                Options = new List<string> { "True", "False" },
                CorrectAnswerIndex = 1,
                Explanation = "Using the same password increases your vulnerability. Always use unique passwords for each account."
            },
            new QuizQuestion
            {
                Question = "Which of the following is a strong password?",
                Options = new List<string>
                {
                    "12345678",
                    "password",
                    "John1990",
                    "T8g$7k!B@z"
                },
                CorrectAnswerIndex = 3,
                Explanation = "Strong passwords include uppercase, lowercase, numbers, and symbols and are hard to guess."
            },
            new QuizQuestion
            {
                Question = "What does two-factor authentication (2FA) do?",
                Options = new List<string>
                {
                    "Uses your IP address to log in",
                    "Requires two different methods to verify your identity",
                    "Automatically updates your password",
                    "Blocks spam emails"
                },
                CorrectAnswerIndex = 1,
                Explanation = "2FA adds a second layer of security by requiring a second form of verification, like a code sent to your phone."
            },
            new QuizQuestion
            {
                Question = "True or False: Clicking on unknown email links is safe if your antivirus is installed.",
                Options = new List<string> { "True", "False" },
                CorrectAnswerIndex = 1,
                Explanation = "Even with antivirus, clicking unknown links can expose you to phishing or malware."
            },
            new QuizQuestion
            {
                Question = "Which of these is a sign of a phishing email?",
                Options = new List<string>
                {
                    "It comes from your bank's official address",
                    "It has poor grammar and urgent requests",
                    "It was expected from a known sender",
                    "It contains no links or attachments"
                },
                CorrectAnswerIndex = 1,
                Explanation = "Phishing emails often contain poor grammar, urgent language, and suspicious links or attachments."
            },
            new QuizQuestion
            {
                Question = "Why is public Wi-Fi risky?",
                Options = new List<string>
                {
                    "It charges extra fees",
                    "It uses more battery",
                    "It can be used to intercept your data",
                    "It is slower than private networks"
                },
                CorrectAnswerIndex = 2,
                Explanation = "Hackers can easily intercept data on unsecured public Wi-Fi networks."
            },
            new QuizQuestion
            {
                Question = "True or False: You should install software updates regularly.",
                Options = new List<string> { "True", "False" },
                CorrectAnswerIndex = 0,
                Explanation = "Software updates often include security patches to protect against known vulnerabilities."
            },
            new QuizQuestion
            {
                Question = "Which action can help protect your privacy online?",
                Options = new List<string>
                {
                    "Sharing your location on all social media posts",
                    "Using private or incognito browsing mode",
                    "Accepting all cookies on every site",
                    "Posting your phone number publicly"
                },
                CorrectAnswerIndex = 1,
                Explanation = "Using private browsing helps reduce tracking and protects your browsing activity."
            },
            new QuizQuestion
            {
                Question = "What is social engineering in cybersecurity?",
                Options = new List<string>
                {
                    "Using social media for marketing",
                    "Building safe network systems",
                    "Manipulating people into revealing confidential information",
                    "Engineering security devices"
                },
                CorrectAnswerIndex = 2,
                Explanation = "Social engineering tricks users into giving away personal information through deception."
            }
            };
        }

        public List<QuizQuestion> LoadQuestions1()
        {
            questions = new List<QuizQuestion>
            { new QuizQuestion
            {
                Question = "What is phishing?",
                Options = new List<string>
                {
                    "A way to catch fish using the internet",
                    "An attempt to obtain sensitive information by pretending to be a trustworthy entity",
                    "A software update",
                    "A type of firewall"
                },
                CorrectAnswerIndex = 1,
                Explanation = "Phishing is a social engineering attack often used to steal user data, such as login credentials or credit card numbers."
            },
            new QuizQuestion
            {
                Question = "True or False: You should use the same password for all your online accounts.",
                Options = new List<string> { "True", "False" },
                CorrectAnswerIndex = 1,
                Explanation = "Using the same password increases your vulnerability. Always use unique passwords for each account."
            },
            new QuizQuestion
            {
                Question = "Which of the following is a strong password?",
                Options = new List<string>
                {
                    "12345678",
                    "password",
                    "John1990",
                    "T8g$7k!B@z"
                },
                CorrectAnswerIndex = 3,
                Explanation = "Strong passwords include uppercase, lowercase, numbers, and symbols and are hard to guess."
            },
            new QuizQuestion
            {
                Question = "What does two-factor authentication (2FA) do?",
                Options = new List<string>
                {
                    "Uses your IP address to log in",
                    "Requires two different methods to verify your identity",
                    "Automatically updates your password",
                    "Blocks spam emails"
                },
                CorrectAnswerIndex = 1,
                Explanation = "2FA adds a second layer of security by requiring a second form of verification, like a code sent to your phone."
            },
            new QuizQuestion
            {
                Question = "True or False: Clicking on unknown email links is safe if your antivirus is installed.",
                Options = new List<string> { "True", "False" },
                CorrectAnswerIndex = 1,
                Explanation = "Even with antivirus, clicking unknown links can expose you to phishing or malware."
            },
            new QuizQuestion
            {
                Question = "Which of these is a sign of a phishing email?",
                Options = new List<string>
                {
                    "It comes from your bank's official address",
                    "It has poor grammar and urgent requests",
                    "It was expected from a known sender",
                    "It contains no links or attachments"
                },
                CorrectAnswerIndex = 1,
                Explanation = "Phishing emails often contain poor grammar, urgent language, and suspicious links or attachments."
            },
            new QuizQuestion
            {
                Question = "Why is public Wi-Fi risky?",
                Options = new List<string>
                {
                    "It charges extra fees",
                    "It uses more battery",
                    "It can be used to intercept your data",
                    "It is slower than private networks"
                },
                CorrectAnswerIndex = 2,
                Explanation = "Hackers can easily intercept data on unsecured public Wi-Fi networks."
            },
            new QuizQuestion
            {
                Question = "True or False: You should install software updates regularly.",
                Options = new List<string> { "True", "False" },
                CorrectAnswerIndex = 0,
                Explanation = "Software updates often include security patches to protect against known vulnerabilities."
            },
            new QuizQuestion
            {
                Question = "Which action can help protect your privacy online?",
                Options = new List<string>
                {
                    "Sharing your location on all social media posts",
                    "Using private or incognito browsing mode",
                    "Accepting all cookies on every site",
                    "Posting your phone number publicly"
                },
                CorrectAnswerIndex = 1,
                Explanation = "Using private browsing helps reduce tracking and protects your browsing activity."
            },
            new QuizQuestion
            {
                Question = "What is social engineering in cybersecurity?",
                Options = new List<string>
                {
                    "Using social media for marketing",
                    "Building safe network systems",
                    "Manipulating people into revealing confidential information",
                    "Engineering security devices"
                },
                CorrectAnswerIndex = 2,
                Explanation = "Social engineering tricks users into giving away personal information through deception."
            }
            };

            return questions;
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

            bool isCorrect = selectedIndex == question.CorrectAnswerIndex;
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

        //public bool CheckAnswer(string selectedOption)
        //{
        //    if (currentQuestionIndex < questions.Count)
        //    {
        //        var currentQuestion = questions[currentQuestionIndex];
        //        return currentQuestion.CorrectAnswer == selectedOption;
        //    }
        //    return false;
        //}
    }
}
