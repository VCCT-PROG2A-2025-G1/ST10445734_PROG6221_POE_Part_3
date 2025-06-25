using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ST10445734_Prog6221_POE_Part1
{

    public static class ChatbotResponses
    {
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

                // Check if the input contains specific keywords to determine the user's interest or concern
                if (input.Contains("interested in") && (input.Contains("password") || input.Contains("phishing") || input.Contains("safe browsing") || input.Contains("privacy")))
                {
                    if (input.Contains("password"))
                    {
                        user.FavoriteTopic = "password";
                        currentTopic = "password";
                    }
                    else if (input.Contains("phishing"))
                    {
                        user.FavoriteTopic = "phishing";
                        currentTopic = "phishing";
                    }
                    else if (input.Contains("safe browsing"))
                    {
                        user.FavoriteTopic = "safe browsing";
                        currentTopic = "safe browsing";
                    }
                    else if (input.Contains("privacy"))
                    {
                        user.FavoriteTopic = "privacy";
                        currentTopic = "privacy";
                    }

                    if (!string.IsNullOrEmpty(currentTopic))
                    {
                        ChatbotResponse($"Great, {user.Name}! I see you're interested in {user.FavoriteTopic}. Would you like to know more about it?");
                        GiveCurrentTipTopic(user.FavoriteTopic);
                    }
                    else
                    {
                        ChatbotResponse("Please specify a topic like password safety, phishing, safe browsing, or privacy.");
                    }
                }
                else if (input.Contains("worried") || input.Contains("scared") || input.Contains("nervous") || input.Contains("concerned"))
                {
                    if (input.Contains("password"))
                    {
                        user.FavoriteTopic = "password";
                        currentTopic = "password";
                    }
                    else if (input.Contains("phishing"))
                    {
                        user.FavoriteTopic = "phishing";
                        currentTopic = "phishing";
                    }
                    else if (input.Contains("safe browsing") || input.Contains("browsing") || input.Contains("browsing safety"))
                    {
                        user.FavoriteTopic = "safe browsing";
                        currentTopic = "safe browsing";
                    }
                    else if (input.Contains("privacy"))
                    {
                        user.FavoriteTopic = "privacy";
                        currentTopic = "privacy";
                    }
                    ChatbotResponse($"It's completely understandable to feel that way, {user.Name}. Cyber threats can be scary. Let's take a look at a helpful tip about {currentTopic}.");
                    GiveCurrentTipTopic(currentTopic);
                }
                else if (input.Contains("curious") || input.Contains("interested") || input.Contains("want to learn"))
                {
                    if (input.Contains("password"))
                    {
                        user.FavoriteTopic = "password";
                        currentTopic = "password";
                    }
                    else if (input.Contains("phishing"))
                    {
                        user.FavoriteTopic = "phishing";
                        currentTopic = "phishing";
                    }
                    else if (input.Contains("safe browsing") || input.Contains("browsing") || input.Contains("browsing safety"))
                    {
                        user.FavoriteTopic = "safe browsing";
                        currentTopic = "safe browsing";
                    }
                    else if (input.Contains("privacy"))
                    {
                        user.FavoriteTopic = "privacy";
                        currentTopic = "privacy";
                    }
                    ChatbotResponse($"That's great, {user.Name}! Learning about {currentTopic} is a smart move. Here's something useful:");
                    GiveCurrentTipTopic(currentTopic);
                }
                else if (input.Contains("frustrated") || input.Contains("annoyed") || input.Contains("confused"))
                {
                    if (input.Contains("password"))
                    {
                        user.FavoriteTopic = "password";
                        currentTopic = "password";
                    }
                    else if (input.Contains("phishing"))
                    {
                        user.FavoriteTopic = "phishing";
                        currentTopic = "phishing";
                    }
                    else if (input.Contains("safe browsing") || input.Contains("browsing") || input.Contains("browsing safety"))
                    {
                        user.FavoriteTopic = "safe browsing";
                        currentTopic = "safe browsing";
                    }
                    else if (input.Contains("privacy"))
                    {
                        user.FavoriteTopic = "privacy";
                        currentTopic = "privacy";
                    }
                    ChatbotResponse($"I'm sorry you're feeling that way, {user.Name}. Let me try to help make {currentTopic} clearer with this tip:");
                    GiveCurrentTipTopic(currentTopic);
                }
                else if (input.Contains("password"))
                {
                    currentTopic = "password";
                    SelectedRandomResponse(passwordTips, currentTopic);
                }
                else if (input.Contains("phishing") || input.Contains("scam"))
                {
                    currentTopic = "phishing";
                    SelectedRandomResponse(phishingTips, currentTopic);
                }
                else if (input.Contains("safe browsing") || input.Contains("browsing") || input.Contains("browsing safety"))
                {
                    currentTopic = "safe browsing";
                    SelectedRandomResponse(browsingTips, currentTopic);
                }
                else if (input.Contains("privacy"))
                {
                    currentTopic = "privacy";
                    SelectedRandomResponse(privacyTips, currentTopic);
                }
                else if (input.Contains("how are you"))
                {
                    ChatbotResponse("I'm doing well, thank you! Always ready to help answer your questions on Cybersecurity.");
                }
                else if (input.Contains("what is your purpose") || input.Contains("what's your purpose"))
                {
                    ChatbotResponse("I am a cybersecutiy awareness bot. I am here to answer questions and give tips about cybersecurity.");
                }
                else if (input.Contains("what can i ask"))
                {
                    ChatbotResponse("You can ask about password safety, phishing scams, browsing safety or anything related to cybersecurity.");
                }
                else if ( input.Contains("again") || input.Contains("repeat") || input.Contains("come again"))
                {
                    if (!string.IsNullOrEmpty(currentTopic))
                    {
                        switch (currentTopic)
                        {
                            case "password":
                                RepeatResponse(passwordTips, currentTopic);
                                break;
                            case "phishing":
                                RepeatResponse(phishingTips, currentTopic);
                                break;
                            case "safe browsing":
                                RepeatResponse(browsingTips, currentTopic);
                                break;
                            case "privacy":
                                RepeatResponse(privacyTips, currentTopic);
                                break;
                        }
                    }
                    else
                    {
                        ChatbotResponse("Please specify a topic first, like password safety, phishing, safe browsing, or privacy.");
                    }
                }
                else if (input.Contains("more") || input.Contains("another") || input.Contains("more info")
                    || input.Contains("explain") || input.Contains("details"))
                {
                    if (!string.IsNullOrEmpty(currentTopic))
                    {
                        ChatbotResponse($"Sure {user.Name}, here's more info on {currentTopic}:");
                        ShowExpandedTip(currentTopic);
                    }
                    else
                    {
                        ChatbotResponse("Please mention a topic first, like password, phishing, privacy, or safe browsing.");
                    }
                }

                else if (input.Contains("remind me") || input.Contains("favourite") || input.Contains("favorite"))
                {
                    if (!string.IsNullOrEmpty(user.FavoriteTopic))
                    {
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
                    ChatbotResponse($"Hello {user.Name}, how can I assist you today?");
                }
                else if (input.Contains("help") || input.Contains("commands"))
                {
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
                Console.ForegroundColor = ConsoleColor.Blue;
                TypeEffect($">> {message}");
                Console.ResetColor();
                Console.WriteLine();
            } 
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                TypeEffect($">> {ex.Message}");
                Console.ResetColor();
                Console.WriteLine();
            }
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
        private static void SelectedRandomResponse(List<string> response, string topic)
        {
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
        private static void RepeatResponse(List<string> response, string topic) 
        {
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

        // method to give the current topic response
        private static void GiveCurrentTipTopic(string topic)
        {
            // This method gives a response based on the current topic of interest
            switch (topic)
            {
                case "password":
                    SelectedRandomResponse(passwordTips, currentTopic);
                    break;
                case "phishing":
                    SelectedRandomResponse(phishingTips, currentTopic);
                    break;
                case "safe browsing":
                    SelectedRandomResponse(browsingTips, currentTopic);
                    break;
                case "privacy":
                    SelectedRandomResponse(privacyTips, currentTopic);
                    break;
            }
        }
    }
}
