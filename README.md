# Cybersecurity Awareness Chatbot - PROG6221 POE

##  Project Overview

Welcome to the Prog6221 POE, a Cybersecurity chatbot. It is a .Net Framework GUI chatbot designed to answer basic questions about cybersecurity.
This chatbot is intended to be a fun and interactive way to learn about cybersecurity concepts and best practices.

## Project Info
- **Student Name:** Thabo Setsubi
- **Student Number:** ST10445734
- **Module:** PROG6221
- **Group:** 01
- **Project:** POE Part 3

##  Key Features

###  Natural Language Processing (NLP)
- Recognizes phrases like “I’m worried about phishing” or “Tell me more about password safety”
- Detects sentiment and intent
- Handles varied, flexible inputs using `Regex`

###  Task Assistant with Reminders
- Add tasks using natural language:  
  e.g. “Remind me to change my password tomorrow”
- Update reminder dates
- View or delete tasks
- Reminders are automatically parsed using date detection

###  Cybersecurity Quiz Game
- Start by typing: “Start quiz” or “Play game”
- Includes **10+ questions** on:
  - Password safety
  - Phishing
  - Safe browsing
  - Social engineering
- Mix of **multiple choice** and **true/false** formats
- Tracks user score and gives feedback

###  Activity Log
- Logs important events:
  - Tasks created, updated, deleted
  - Quiz attempts
  - NLP interactions (e.g. favorite topics)
- Viewable on command:  
  “Show activity log” or “What have you done for me?”

---

## Technologies Used

- **C#** with **.NET Framework 4.8**
- **WPF (Windows Presentation Foundation)** for the GUI
- **Regex** for NLP and command detection
- **Custom services** for managing tasks, quizzes, and activity log
- **OOP principles**: separation of concerns, single-responsibility

---

## How to Use

1. **Start the Application** (Visual Studio or standalone EXE)
2. Type into the chatbot box:
   - Ask cybersecurity questions:  
     `"Tell me about phishing"`
   - Add a reminder:  
     `"Remind me to update my antivirus next week"`
   - Start quiz:  
     `"Start the quiz"`
   - View activity:  
     `"Show activity log"`

3. **Check the display area** for all feedback and responses.
4. Tasks and logs persist for your session.

---
## Getting Started

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/yourusername/CybersecurityChatbot.git
2. **Open in Visual Studio:**
  - Target Framework: .NET Framework 4.8
  - Run the MainWindow.xaml

4. **Run the App:**
  - Interact with the chatbot via the input box.




## Video Presentation Link
- https://youtu.be/2PtgerBzjRU
