using ST10445734_PROG6221_POE_Part_3;
using ST10445734_PROG6221_POE_Part_3.Models;
using ST10445734_PROG6221_POE_Part_3.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Task = ST10445734_PROG6221_POE_Part_3.Task;

namespace ST10445734_Prog6221_POE_Part1
{

    public static class ChatbotResponses
    {

        public static Action<string> ChatOutput = null; // Action to output chat messages

        private static TaskService taskService = new TaskService(); // Instance of TaskService to manage tasks
        private static QuizService quizService = new QuizService(); // Instance of QuizService to manage quizzes

        private static bool isQuizActive = false; // Flag to indicate if a quiz is currently active
        private static int currentQuestionIndex = 0; // Index of the current question in the quiz
        private static int score = 0; // Variable to keep track of the user's score in the quiz
        private static int quizAttempts = 0; // Counter for the number of quiz attempts
        private static List<QuizQuestion> quizQuestions; // List to hold the quiz questions

        private static List<string> activityLog = new List<string>();
        private static LogEntry currentLogEntry = new LogEntry(); // Current log entry being created
        private static List<LogEntry> logEntries = new List<LogEntry>(); // List to hold log entries

        private static Random rand = new Random();
        private static List<string> passwordTips = new List<string>
        {
            "Use strong, unique passwords for each of your accounts.",
            "Avoid using personal information in passwords, like your birthdate or name.",
            "Use a password manager to securely store and manage your passwords.",
            "Change your passwords regularly and don’t reuse them.",
            "Enable two-factor authentication where possible."
        };
        private static List<string> phishingTips = new List<string>
        {
            "Never click on suspicious links or attachments from unknown sources.",
            "Look out for poor grammar and urgent language in emails—it could be a scam.",
            "Verify the sender's email address before responding or clicking any links.",
            "Legitimate organizations won’t ask for sensitive info via email.",
            "Use anti-phishing browser extensions to protect yourself online."
        };
        private static List<string> browsingTips = new List<string>
        {
           "Make sure websites use HTTPS before entering personal information.",
            "Keep your browser and plugins updated to avoid security flaws.",
            "Use an ad blocker to avoid malicious ads.",
            "Avoid using public Wi-Fi for sensitive transactions.",
            "Use incognito or private mode when necessary."
        };
        private static List<string> privacyTips = new List<string>
        {
            "Review and adjust your social media privacy settings regularly.",
            "Avoid sharing too much personal information online.",
            "Use encrypted messaging apps for private communication.",
            "Be mindful of app permissions on your phone.",
            "Clear your browser cookies and history frequently."
        };

        private static string currentTopic = "";

        private static Dictionary<string, int> lastIndicesUsed = new Dictionary<string, int>();
        private static Dictionary<string, int> expandedTipIndex = new Dictionary<string, int>();
        public static Dictionary<string, List<string>> Tips = new Dictionary<string, List<string>>
        {
            { "password", new List<string>
                {
                    "Use strong, unique passwords for each of your accounts. A strong password typically includes a mix of upper and lowercase letters, numbers, and special characters.",
                    "Avoid using personal information in passwords, like your birthdate or name. Hackers often try these details first when guessing passwords.",
                    "Use a password manager to securely store and manage your passwords. These tools generate and store complex passwords for you.",
                    "Change your passwords regularly and don’t reuse them. Regular changes reduce the risk of long-term breaches.",
                    "Enable two-factor authentication where possible. This adds a second layer of protection to your accounts."
                }
            },
            { "phishing", new List<string>
                {
                    "Never click on suspicious links or attachments from unknown sources. These could contain malware or redirect to fake sites.",
                    "Look out for poor grammar and urgent language in emails—it could be a scam. Phishing messages often try to create panic.",
                    "Verify the sender's email address before responding or clicking links. Fraudulent addresses can look nearly identical to real ones.",
                    "Legitimate organizations won’t ask for sensitive info via email. Be wary of any requests for passwords or financial details.",
                    "Use anti-phishing browser extensions to protect yourself online. These can block known scam websites."
                }
            },
            { "safe browsing", new List<string>
                {
                    "Make sure websites use HTTPS before entering personal information. Look for the padlock icon in the browser address bar.",
                    "Keep your browser and plugins updated to avoid security flaws. Updates patch known vulnerabilities.",
                    "Use an ad blocker to avoid malicious ads. Some ads may lead to phishing sites or download malware.",
                    "Avoid using public Wi-Fi for sensitive transactions. Public networks are often unsecured and risky.",
                    "Use incognito or private mode when necessary. This reduces local tracking and temporary storage of browsing data."
                }
            },
            { "privacy", new List<string>
                {
                    "Review and adjust your social media privacy settings regularly. Ensure only trusted individuals can see your content.",
                    "Avoid sharing too much personal information online. Oversharing increases your risk of identity theft and scams.",
                    "Use encrypted messaging apps for private communication. Apps like Signal ensure only the sender and receiver can read messages.",
                    "Be mindful of app permissions on your phone. Only allow necessary permissions to prevent misuse of your data.",
                    "Clear your browser cookies and history frequently. This helps reduce tracking and personalized advertising."
                }
            }
        };

        // method to respond to user input
        public static void RespondToInput(string input, User user)
        {
            // This method processes user input and responds with appropriate tips or information based on the user's interests and queries.
            try
            {
                // Check if the input is null or empty, and if so, prompt the user to type something
                if (string.IsNullOrWhiteSpace(input))
                {
                    ChatbotResponse($"Sorry, I didn't catch that {user.Name}. Could you type something?");
                }
                // Check if the user is null, which means the user has not been initialized or found
                if (user == null) 
                {
                    ChatbotResponse("User not found. Please start over.");
                }

                input = input.ToLower(); // Convert input to lowercase for easier matching

                if (isQuizActive) 
                {
                    EvaluateQuizAnswer(input);
                    return; // Exit if the quiz is active
                }

                // Check if the input contains specific keywords to determine the user's interest or concern
                if (Regex.IsMatch(input, @"\b(interested in|want to know more about|keen on|tell me more)\b.*\b(password|phishing|safe browsing|privacy)\b"))
                {
                    string matchedTopic = GetMatchedTopic(input);

                    if (!string.IsNullOrEmpty(matchedTopic))
                    {
                        user.FavoriteTopic = matchedTopic; // Set the user's favorite topic based on the matched topic
                        currentTopic = matchedTopic; // Update the current topic
                        LogActivity($"{user.Name} is interested about {currentTopic}");

                        ChatbotResponse($"Great, {user.Name}! I see you're interested in {user.FavoriteTopic}. Would you like to know more about it?");
                        GiveCurrentTipTopic(user.FavoriteTopic);
                    }
                    else
                    {
                        ChatbotResponse("Please specify a topic like password safety, phishing, safe browsing, or privacy.");
                    }
                }
                else if (Regex.IsMatch(input, @"\b(worried|afraid|concerned|nervous|scared)\b.*\b(password|phishing|safe browsing|privacy)\b"))
                {
                    string matchedTopic = GetMatchedTopic(input);

                    if (!string.IsNullOrEmpty(matchedTopic))
                    {
                        user.FavoriteTopic = matchedTopic; // Set the user's favorite topic based on the matched topic
                        currentTopic = matchedTopic; // Update the current topic

                        LogActivity($"{user.Name} is concerned about {currentTopic}");
                        ChatbotResponse($"It's completely understandable to feel that way, {user.Name}. Cyber threats can be scary. Let's take a look at a helpful tip about {currentTopic}.");
                        GiveCurrentTipTopic(user.FavoriteTopic);
                    }
                    else
                    {
                        ChatbotResponse("Please specify a topic like password safety, phishing, safe browsing, or privacy.");
                    }

                    GiveCurrentTipTopic(currentTopic);
                }
                else if (Regex.IsMatch(input, @"\b(curious|want to learn)\b.*\b(password|phishing|safe browsing|privacy)\b")) // input.Contains("curious") || input.Contains("interested") || input.Contains("want to learn")
                {
                    string matchedTopic = GetMatchedTopic(input);

                    if (!string.IsNullOrEmpty(matchedTopic))
                    {
                        user.FavoriteTopic = matchedTopic; // Set the user's favorite topic based on the matched topic
                        currentTopic = matchedTopic; // Update the current topic

                        LogActivity($"{user.Name} is curious about {currentTopic}");
                        ChatbotResponse($"That's great, {user.Name}! Learning about {currentTopic} is a smart move. Here's something useful:");
                        GiveCurrentTipTopic(currentTopic);
                    }
                    else
                    {
                        ChatbotResponse("Please specify a topic like password safety, phishing, safe browsing, or privacy.");
                    }
                }
                else if (Regex.IsMatch(input, @"\b(frustrated|annoyed|confused)\b.*\b(password|phishing|safe browsing|privacy)\b")) // input.Contains("frustrated") || input.Contains("annoyed") || input.Contains("confused")
                {
                    string matchedTopic = GetMatchedTopic(input);

                    if (!string.IsNullOrEmpty(matchedTopic))
                    {
                        user.FavoriteTopic = matchedTopic; // Set the user's favorite topic based on the matched topic
                        currentTopic = matchedTopic; // Update the current topic

                        LogActivity($"{user.Name} is frustrated about {currentTopic}");
                        ChatbotResponse($"I'm sorry you're feeling that way, {user.Name}. Let me try to help make {currentTopic} clearer with this tip:");
                        GiveCurrentTipTopic(currentTopic);
                    }
                    else
                    {
                        ChatbotResponse("Please specify a topic like password safety, phishing, safe browsing, or privacy.");
                    }
                }
                else if (Regex.IsMatch(input, @"\b(add|create|set|schedule|make)\b.*\b(task|reminder)\b", RegexOptions.IgnoreCase) ||
                    Regex.IsMatch(input, @"\b(remind me of|can you remind me of|remind me to)\b", RegexOptions.IgnoreCase) ||
                    Regex.IsMatch(input, @"\b(add task to|set task to|create task for|make this a task)\b", RegexOptions.IgnoreCase))
                {

                    currentTopic = "task"; // Set current topic to task

                    string taskTitle = ExtractTaskTitle(input); // Extract the task title from the input

                    Task newTask = new Task()
                    {
                        Title = taskTitle, // Use the input as the task title
                        Description = "No description provided", // Default description
                        ReminderDate = ParseNaturalDate(input) ?? DateTime.Now.AddDays(1),
                        IsCompleted = false
                    }; // Create a new task object

                    taskService.AddTask(newTask); // Add the new task using the TaskService
                    ChatbotResponse($"Got it! Adding, {taskTitle}, to task list.");
                    LogActivity($"Task added: '{newTask.Title}' (Reminder: {newTask.ReminderDate})");
                }
                else if (Regex.IsMatch(input, @"\b(remind me in|update the date|set a reminder)\b\s+(a\s+)?task", RegexOptions.IgnoreCase))
                {
                    Match match = Regex.Match(input, @"for (.+?) to (\d{4}-\d{2}-\d{2})", RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        string taskTitle = match.Groups[1].Value.Trim(); // Extract the task title from the input
                        DateTime reminderDate = (DateTime)ParseNaturalDate(input);
                        if (DateTime.TryParse(match.Groups[2].Value, out reminderDate)) // Try to parse the date
                        {
                            Task existingTask = taskService.GetTasks().FirstOrDefault(t => t.Title.Equals(taskTitle, StringComparison.OrdinalIgnoreCase));
                            if (existingTask != null)
                            {
                                LogActivity($"Updated info on task called: '{existingTask.Title}'");
                                existingTask.ReminderDate = reminderDate; // Update the reminder date for the existing task
                                ChatbotResponse($"Reminder for '{taskTitle}' has been updated to {reminderDate.ToShortDateString()}.");
                            }
                            else
                            {
                                ChatbotResponse($"No task found with the title '{taskTitle}'. Please check the title and try again.");
                            }
                        }
                        else
                        {
                            ChatbotResponse("Invalid date format. Please use YYYY-MM-DD format.");
                        }
                    }
                    else
                    {
                        ChatbotResponse("Please specify a task title and a date in the format 'YYYY-MM-DD'.");
                    }
                }
                else if (input.Contains("how are you") || input.Contains("how you doing") || input.Contains("feeling alright"))
                {
                    LogActivity($"{user.Name} asked how the I'm doing");

                    ChatbotResponse("I'm doing well, thank you! Always ready to help answer your questions on Cybersecurity.");
                }
                else if (input.Contains("what is your purpose") || input.Contains("what's your purpose") || input.Contains("do you have a purpose"))
                {
                    LogActivity($"{user.Name} asked about my purpose in life.");
                    ChatbotResponse("I am a cybersecutiy awareness bot. I am here to answer questions and give tips about cybersecurity.");
                }
                else if (input.Contains("what can i ask"))
                {
                    LogActivity($"{user.Name} asked me what can the ask from me");
                    ChatbotResponse("You can ask about password safety, phishing scams, browsing safety or anything related to cybersecurity.");
                }
                else if (Regex.IsMatch(input, @"\b(again|repeat|come again|please repeat|can you explain again|huh)\b.*\b(password|phishing|safe browsing|privacy)\b")) // input.Contains("again") || input.Contains("repeat") || input.Contains("come again")
                {
                    string matchedTopic = GetMatchedTopic(input);

                    if (!string.IsNullOrEmpty(currentTopic))
                    {
                        LogActivity($"Repeated information on {matchedTopic}");
                        currentTopic = matchedTopic; // Update the current topic
                        RepeatResponse(currentTopic); // Repeat the last response for the current topic
                    }
                    else
                    {
                        ChatbotResponse("Please specify a topic first, like password safety, phishing, safe browsing, or privacy.");
                    }
                }
                else if (Regex.IsMatch(input, @"\b(more info|tell me more|expand on this|tell me more about|can you explain again|explain more|give me more information)\b.*\b(password|phishing|safe browsing|privacy)\b")) // input.Contains("more info") || input.Contains("tell me more") || input.Contains("expand on this") || input.Contains("explain more")  || input.Contains("tell me more about") || input.Contains("expand on this") || input.Contains("explain more"))
                {
                    string matchedTopic = GetMatchedTopic(input);

                    if (!string.IsNullOrEmpty(currentTopic))
                    {
                        LogActivity($"Gave more information on {matchedTopic}");
                        ChatbotResponse($"Sure {user.Name}, here's more info on {currentTopic}:");
                        ShowExpandedTip(currentTopic);
                    }
                    else
                    {
                        ChatbotResponse("Please mention a topic first, like password, phishing, privacy, or safe browsing.");
                    }
                }

                else if (Regex.IsMatch(input, @"\b(remind me|favourite|expand on this|favorite|love this|enjoy)\b.*\b(password|phishing|safe browsing|privacy)\b")) // input.Contains("remind me") || input.Contains("favourite") || input.Contains("favorite")
                {
                    string matchedTopic = GetMatchedTopic(input);

                    if (!string.IsNullOrEmpty(matchedTopic))
                    {
                        user.FavoriteTopic = matchedTopic; // Set the user's favorite topic based on the matched topic
                        currentTopic = matchedTopic; // Update the current topic

                        LogActivity($"Told the user about their favourite topic {matchedTopic}");
                        ChatbotResponse($"As someone interested in {user.FavoriteTopic}");
                        GiveCurrentTipTopic(user.FavoriteTopic);
                    }
                    else
                    {
                        ChatbotResponse("You haven’t told me your favorite topic yet. Try saying, 'I'm interested in phishing'.");
                    }
                }

                else if (input.Contains("hi") || input.Contains("hello") || input.Contains("hey"))
                {
                    LogActivity($"Greeted the user, {user.Name}");
                    ChatbotResponse($"Hello {user.Name}, how can I assist you today?");
                }
                else if (input.Contains("help"))
                {
                    LogActivity($"User, {user.Name}, asked to eloborate on what I can help them with");
                    ChatbotResponse("You can ask me about password safety, phishing scams, safe browsing, or privacy tips. Just type your question or topic of interest.");
                }
                else if (input.Contains("commands") || input.Contains("menu") || input.Contains("menu again"))
                {
                    ChatbotResponse(" - How are you?\n" +
                    " - What's your purpose?\n" +
                    " - What can I ask you about?\n" +
                    " - Password safety\n" +
                    " - Phishing\n" +
                    " - Safe Browsing\n" +
                    " - Or type 'Exit'");
                }
                else if (Regex.IsMatch(input, @"\b(show the|display|show all|view all|list)\b.*\btasks?\b", RegexOptions.IgnoreCase))
                {
                    taskService.GetTasks(); // Get the list of tasks from the TaskService
                    LogActivity($"Generated list of all the tasks");
                    List<Task> tasks = taskService.GetTasks(); // Get the list of tasks

                    foreach (var task in tasks)
                    {
                        string status = task.IsCompleted ? "Completed" : "Pending";
                        string reminder = task.ReminderDate.HasValue ? task.ReminderDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : "No reminder set";
                        ChatbotResponse($"Task: {task.Title}, Description: {task.Description}, Status: {status}, Reminder: {reminder}");
                    }
                }
                else if (Regex.IsMatch(input, @"\b(delete|remove|get rid of)\b.*\btask\b", RegexOptions.IgnoreCase))
                {
                    Match match = Regex.Match(input, @"task (called|named)? (.+)", RegexOptions.IgnoreCase); // Match the input to find the task title to delete
                    if (match.Success)
                    {
                        string titleToRemove = match.Groups[2].Value.Trim();

                        var task = taskService.GetTasks().FirstOrDefault(t => t.Title.Equals(titleToRemove, StringComparison.OrdinalIgnoreCase));
                        if (task != null)
                        {
                            taskService.DeleteTask(task);
                            LogActivity($"Task \"{titleToRemove}\" has been removed.");
                            ChatbotResponse($"Task \"{titleToRemove}\" has been removed.");
                        }
                        else
                        {
                            ChatbotResponse($"I couldn't find a task with the title \"{titleToRemove}\".");
                        }
                    }
                    else
                    {
                        ChatbotResponse("Please specify the task to delete, e.g., 'Delete task called update antivirus'.");
                    }
                }
                else if (Regex.IsMatch(input, @"\b(done|done with|as completed|i completed the|completed|finish|finished)\b.*\btask\b", RegexOptions.IgnoreCase)) 
                {
                    Match match = Regex.Match(input, @"task (called|named)? (.+)", RegexOptions.IgnoreCase); // Match the input to find the task title to mark as completed
                    if (match.Success)
                    {
                        string titleToComplete = match.Groups[2].Value.Trim();
                        var task = taskService.GetTasks().FirstOrDefault(t => t.Title.Equals(titleToComplete, StringComparison.OrdinalIgnoreCase));
                        if (task != null)
                        {
                            taskService.MarkTaskAsCompleted(task);
                            LogActivity($"Task \"{titleToComplete}\" has been marked as completed.");
                            ChatbotResponse($"Task \"{titleToComplete}\" has been marked as completed.");
                        }
                        else
                        {
                            ChatbotResponse($"I couldn't find a task with the title \"{titleToComplete}\".");
                        }
                    }
                    else
                    {
                        ChatbotResponse("Please specify the task to mark as completed, e.g., 'Mark task called update antivirus as completed'.");
                    }
                }
                else if (Regex.IsMatch(input, @"\b(clear|delete all|remove all)\b.*\b(tasks?|reminders?)\b", RegexOptions.IgnoreCase))
                {
                    taskService.ClearAllTasks(); // Clear all tasks using the TaskService
                    LogActivity("All tasks have been cleared.");
                    ChatbotResponse("All tasks have been cleared.");
                }
                else if (Regex.IsMatch(input, @"\b(start|play|begin|launch)\b.*\b(quiz|game|cybersecurity quiz|cybersecurity game)\b", RegexOptions.IgnoreCase) ||
                    Regex.IsMatch(input, @"\b(quiz|test|game)\b", RegexOptions.IgnoreCase))
                {
                    ChatbotResponse($"Great, {user.Name}! Let's start a cybersecurity quiz. I will ask you a series of questions, and you can answer them to test your knowledge.");

                    

                    isQuizActive = true; // Set the quiz active flag to true
                    quizAttempts++; // Increment the quiz attempts counter

                    LogActivity($"{user.Name} started a quiz on cybersecurity awareness. Attempt number: {quizAttempts}");

                    currentQuestionIndex = 0; // Reset the current question index
                    score = 0; // Reset the score
                    quizQuestions = quizService.LoadQuestions1(); // Load the quiz questions from the QuizService

                    AskNextQuestion();

                }
                else if (Regex.IsMatch(input, @"\b(show|view)\b.*\b(activity log|log|history|activity|actions)\b", RegexOptions.IgnoreCase) ||
                    Regex.IsMatch(input, @"\b(what have you done|recent actions|previous tasks)\b", RegexOptions.IgnoreCase))
                {
                    if (logEntries.Count == 0)
                    {
                        ChatbotResponse("No activity log entries found yet. Start interacting with me to create a log of your activities.");
                    }
                    else
                    {
                        ChatbotResponse("Here are your recent activities:");
                        foreach (var entry in logEntries)
                        {
                            ChatbotResponse($"{entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)} - {entry.Message}");
                        }
                    }
                }
                else if (Regex.IsMatch(input, @"\b(password|phishing|safe browsing|privacy)\b"))
                {
                    string matchedTopic = GetMatchedTopic(input);

                    if (!string.IsNullOrEmpty(matchedTopic))
                    {
                        LogActivity($"{user.Name} wants to know about {currentTopic}");
                        currentTopic = matchedTopic; // Update the current topic
                        SelectedRandomResponse(currentTopic);
                    }
                    else
                    {
                        ChatbotResponse("Please specify a topic like password safety, phishing, safe browsing, or privacy.");
                    }
                }
                else
                {
                    ChatbotResponse("Not quite sure what you are asking. Please ask another quesiton");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return;
            }
        }

        // method to play the quiz
        private static void AskNextQuestion()
        {
            if (currentQuestionIndex < quizQuestions.Count)
            {
                var question = quizQuestions[currentQuestionIndex]; // Get the current question
                string formattedQuestion = $"Q{currentQuestionIndex + 1}: {question.Question}\n";

                for (int i = 0; i < question.Options.Count; i++)
                {
                    formattedQuestion += $"{(char)('A' + i)}. {question.Options[i]}\n";
                }

                ChatbotResponse(formattedQuestion);
            }
            else 
            {
                ChatbotResponse($"Quiz complete! You got {score} out of {quizQuestions.Count} correct.");
                isQuizActive = false;
            }
        }

        // method to evaluate the quiz answer
        private static void EvaluateQuizAnswer(string input)
        {
            var question = quizQuestions[currentQuestionIndex]; // Get the current question
            int selectedIndex = GetAnswerFromInput(input);

            var answerOption = question.Options[selectedIndex]; // Get the answer options for the current question

            if (selectedIndex == question.CorrectAnswerIndex)
            {
                score++; // Increment the score if the answer is correct
                ChatbotResponse($"Correct! {question.Explanation}");
            }
            else
            {
                ChatbotResponse($"Incorrect. {question.Explanation}");
            }

            currentQuestionIndex++; // Move to the next question
            AskNextQuestion();
        }

        // method to get the answer index from the input string
        private static int GetAnswerFromInput(string input) 
        {
            input = input.ToLower().Trim(); // Normalize the input to lowercase and trim whitespace

            if (input.StartsWith("a")) return 0; // If input starts with 'a', return index 0
            if (input.StartsWith("b")) return 1; // If input starts with 'b', return index 1
            if (input.StartsWith("c")) return 2; // If input starts with 'c', return index 2
            if (input.StartsWith("d")) return 3; // If input starts with 'd', return index 3

            if (input.Contains("true")) return 0; // If input contains 'true', return index 0
            if (input.Contains("false")) return 1; // If input contains 'false', return index 1 

            return -1; // Default return value if no valid answer is found
        }

        // method to show expanded tip
        private static void ShowExpandedTip(string currentTopic)
        {
            // Check if the current topic has tips available
            if (!Tips.ContainsKey(currentTopic))
            {
                ChatbotResponse("I don’t have more information on that topic right now.");
                return;
            }

            List<string> tips = Tips[currentTopic]; // Get the tips for the current topic
            if (!expandedTipIndex.ContainsKey(currentTopic))
                expandedTipIndex[currentTopic] = 0; // Initialize index if not already set

            int index = expandedTipIndex[currentTopic]; // Get the current index for the topic
            ChatbotResponse(tips[index]);

            // Update index for next time
            expandedTipIndex[currentTopic] = (index + 1) % tips.Count;
        }

        // method to add chatbot standard
        private static void ChatbotResponse(string message)
        {
            // This method formats the chatbot's response with a typing effect and color coding
            try
            {
                if (ChatOutput != null)
                {
                   ChatOutput($">> {message}"); // Set default print method if not already set
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    TypeEffect($">> {message}");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            } 
            catch (Exception ex)
            {
                if (ChatOutput !=null)  
                {
                    ChatOutput($">> ERROR: {ex.Message}"); // If ChatOutput is set, use it to print the error message
                }
                else if (Console.IsOutputRedirected) 
                { 
                    return; // If output is redirected, do not print to console
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    TypeEffect($">> {ex.Message}");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
                
                
        }

        // method to set the print method for chatbot responses
        public static void SetPrintMethod(Action<string> printMethod)
        {
            // This method sets the print method for chatbot responses
            ChatOutput = printMethod;
        }

        // adds typing animation effect
        public static void TypeEffect(string m)
        {
            if (string.IsNullOrEmpty(m)) 
            {
                Console.WriteLine();
            }
            try 
            {
                for (int i = 0; i < m.Length; i++)
                {
                    Console.Write(m[i]);
                    Thread.Sleep(60); // 60ms delay
                }
                Console.WriteLine(); // Move to the next line after typing the message
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error in TypeEffect: {ex.Message}");
                Console.ResetColor();
                return;
            }
        }

        // method to randomise the response from a list of responses
        private static void SelectedRandomResponse( string topic)
        {
            List<string> response = GetResponseListByTopic(topic); // Get the list of responses for the current topic

            // This method selects a random response from the provided list of responses for the given topic
            if (response == null || response.Count == 0) 
            {
                ChatbotResponse("I am still learning about this topic. Please ask another question?"); // No responses available
                return;
            }

            int lastIndex = -1; // This variable will hold the last index used for the topic to avoid repetition
            // Check if the topic has a last index stored, if so, use it to avoid repeating the same response
            if (lastIndicesUsed.ContainsKey(topic)) 
            {
                lastIndex = lastIndicesUsed[topic];
            }

            int newIndex; // Generate a new random index for the response
            // Loop to ensure the new index is different from the last used index
            do
            {
                newIndex = rand.Next(response.Count); // Generate a random index within the range of the response list
            }
            //  This loop continues until a new index is found that is different from the last used index
            while (response.Count > 1 && newIndex == lastIndex);

            lastIndicesUsed[topic] = newIndex; // Update the last index used for the topic with the new index

            ChatbotResponse(response[newIndex]); // Output the selected response to the console
        }

        // method to repeat the last response for a specific topic
        private static void RepeatResponse( string topic) 
        {
            List<string> response = GetResponseListByTopic(topic); // Get the list of responses for the current topic

            // This method repeats the last response for a specific topic
            if (response == null || response.Count == 0)
            {
                ChatbotResponse("I am still learning about this topic. Please ask another question?");
            }

            int lastIndex = -1; // This variable will hold the last index used for the topic to avoid repetition
            // Check if the topic has a last index stored, if so, use it to avoid repeating the same response
            if (lastIndicesUsed.ContainsKey(topic))
            {
                lastIndex = lastIndicesUsed[topic]; 
            }

            int newIndex; // Generate a new random index for the response
            // Loop to ensure the new index is different from the last used index
            do
            {
                newIndex = rand.Next(response.Count); 
            }
            // This loop continues until a new index is found that is different from the last used index
            while (response.Count > 1 && newIndex == lastIndex);

            lastIndicesUsed[topic] = newIndex; // Update the last index used for the topic with the new index

            ChatbotResponse(response[lastIndex]); // Output the selected response to the console
        }

        // method to get the list of responses based on the current topic
        private static List<string> GetResponseListByTopic(string topic) 
        {
            // This method returns the list of responses based on the current topic
            switch (topic)
            {
                case "password":
                    return passwordTips;
                case "phishing":
                    return phishingTips;
                case "safe browsing":
                    return browsingTips;
                case "privacy":
                    return privacyTips;
                default:
                    return new List<string>(); // Return an empty list if no topic matches
            }

        }
            

        // method to give the current topic response
        private static void GiveCurrentTipTopic(string topic)
        {
            // This method gives a response based on the current topic of interest
            switch (topic)
            {
                case "password":
                    SelectedRandomResponse(currentTopic);
                    break;
                case "phishing":
                    SelectedRandomResponse(currentTopic);
                    break;
                case "safe browsing":
                    SelectedRandomResponse(currentTopic);
                    break;
                case "privacy":
                    SelectedRandomResponse(currentTopic);
                    break;
            }
        }

        // method to get the matched topic from the input string
        private static string GetMatchedTopic(string input) 
        {
            if (Regex.IsMatch(input,@"\bpassword(s)?\b")) return "password";
            if (Regex.IsMatch(input,@"\bphishing\b")) return "phishing";
            if (Regex.IsMatch(input, @"\bsafe browsing|browsing safety\b")) return "safe browsing";
            if (Regex.IsMatch(input, @"\bprivacy\b")) return "privacy";
            return null; // Return null if no topic is matched
        }

        // method to extract the task title from the input string
        private static string ExtractTaskTitle(string input) 
        {
            // remove common phrases from the input to extract the task title
            string cleanedInput = Regex.Replace(input, @"^(can you )?(remind me (to|of)|add task to|create task for|set (a )?task to)\s+", "", RegexOptions.IgnoreCase).Trim();

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cleanedInput); // Convert the cleaned input to title case code from chatgpt
        }

        // method to parse natural language date input
        private static DateTime? ParseNaturalDate(string input) 
        {
            if (input.Contains("tomorrow")) 
            {
                return DateTime.Now.AddDays(1); // Return tomorrow's date
            }
            if (input.Contains("next week")) 
            {
                return DateTime.Now.AddDays(7); // Return next week's date
            }

            Match inDaysMatch = Regex.Match(input, @"in (\d+) (day|days|week|weeks)", RegexOptions.IgnoreCase);

            if (inDaysMatch.Success) 
            {
                int amount = int.Parse(inDaysMatch.Groups[1].Value);
                string unit = inDaysMatch.Groups[2].Value;

                if (unit.StartsWith("week"))
                {
                    return DateTime.Now.AddDays(amount * 7);
                }
                else
                {
                    return DateTime.Now.AddDays(amount); // Return the date in the specified number of days
                }
            }

            return null;
        }

        // method to log activity
        private static void LogActivity(string log) 
        {
            LogEntry newLog = new LogEntry 
            {
                Timestamp = DateTime.Now, // Set the current timestamp
                Message = log // Set the log message
            };

            logEntries.Add(newLog); // Add the log entry to the list of log entries

            if (logEntries.Count > 10) 
            {
                logEntries.RemoveAt(0); // Remove the oldest log entry if the list exceeds 10 entries
            }
        }
    }
}
